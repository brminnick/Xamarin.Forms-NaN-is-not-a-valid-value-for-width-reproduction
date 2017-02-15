using System;
using System.IO;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace NaNReproduction
{

	public class AzurePunModel
	{
		public AzurePunModel(int punNumber)
		{
			id = punNumber;
			PunNumber = punNumber;
		}

		public int id { get; }

		public int PunNumber { get; }

		public string PunImageUrl { get; set; }

		public DateTimeOffset PunCreatedDate { get; set; }

		public string PunUrl { get; set; }

		public string PunImageAsBase64String { get; set; }
	}

	public class PuzzleCellCardView : ViewCell
	{
		readonly Image _puzzleImage;
		readonly Label _numberValueLabel;
		readonly Image _questionMarkImage;
		readonly Image _checkImage;

		public PuzzleCellCardView()
		{
			var puzzleImage = new Image
			{
				HeightRequest = 150,
				BackgroundColor = Color.White
			};

			var numberTextLabel = new Label
			{
				Text = " Number",
			};

			_numberValueLabel = new Label();

			var numberStackLayout = new StackLayout
			{
				Children = {
				numberTextLabel,
				_numberValueLabel
			},
				Orientation = StackOrientation.Horizontal,
				BackgroundColor = Color.White
			};

			_questionMarkImage = new Image
			{
				Source = "Check",
				MinimumHeightRequest = 100,
				BackgroundColor = Color.White
			};

			_checkImage = new Image
			{
				Source = "QuestionMark",
				MinimumHeightRequest = 100,
				BackgroundColor = Color.White
			};

			var whitePuzzleNumberBackgroundBoxView = new BoxView
			{
				BackgroundColor = Color.White
			};

			var cellGridLayout = new Grid
			{
				BackgroundColor = Color.Black,
				Padding = new Thickness(2),
				RowSpacing = 2,
				ColumnSpacing = 1,
				VerticalOptions = LayoutOptions.Fill,

				RowDefinitions = {
				new RowDefinition{ Height = new GridLength (20, GridUnitType.Absolute) },
				new RowDefinition{ Height = new GridLength (150, GridUnitType.Absolute) }
			},
				ColumnDefinitions = {
				new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star) },
				new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star) }
			}
			};
			cellGridLayout.Children.Add(whitePuzzleNumberBackgroundBoxView, 0, 0);
			Grid.SetColumnSpan(whitePuzzleNumberBackgroundBoxView, 2);

			cellGridLayout.Children.Add(numberStackLayout, 0, 0);
			Grid.SetColumnSpan(numberStackLayout, 2);

			cellGridLayout.Children.Add(_puzzleImage, 0, 1);

			cellGridLayout.Children.Add(_checkImage, 1, 1);
			cellGridLayout.Children.Add(_questionMarkImage, 1, 1);

			View = cellGridLayout;
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			var isCheckVisible = new Random().Next(0, 1).Equals(0);

			var binding = BindingContext as AzurePunModel;

			_puzzleImage.Source = App.ConvertBase64StringToImageSource(binding.PunImageAsBase64String);
			_checkImage.IsVisible = isCheckVisible;
			_questionMarkImage.IsVisible = !isCheckVisible;
			_numberValueLabel.Text = binding.PunNumber.ToString();
		}
	}

	public class ListViewContentPage : ContentPage
	{
		public ListViewContentPage()
		{
			List<AzurePunModel> azurePunList;

			Task.Run(async () => azurePunList = await GetAzurePunModels()).Wait();

			var listView = new ListView(ListViewCachingStrategy.RecycleElement)
			{
				BackgroundColor = Color.White,
				RowHeight = 200,
				ItemTemplate = new DataTemplate(typeof(PuzzleCellCardView)),
				SeparatorColor = Color.Transparent
			};

			Content = listView;
		}

		async Task<List<AzurePunModel>> GetAzurePunModels()
		{
			var azurePunModelList = await App.GetDataObjectFromAPI<List<AzurePunModel>>("https://mondaypundayappservices.azurewebsites.net/tables/AzurePunModel");
			return azurePunModelList;
		}
	}

	public class App : Application
	{
		public App()
		{
			MainPage = new ListViewContentPage();
		}

		public static ImageSource ConvertBase64StringToImageSource(string imageAsBase64String)
		{
			try
			{
				if (imageAsBase64String == null)
				{
					return null;
				}

				var imageByteArray = Convert.FromBase64String(imageAsBase64String);

				return ImageSource.FromStream(() => new MemoryStream(imageByteArray));
			}
			catch (Exception e)
			{
				return null;
			}
		}

		public static HttpClient CreateHttpClient()
		{
			HttpClient client;

			if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
				client = new HttpClient { Timeout = TimeSpan.FromSeconds(60) };
			else
				client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip })
				{
					Timeout = TimeSpan.FromSeconds(60)
				};

			client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
			client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

			return client;
		}

		static readonly JsonSerializer _serializer = new JsonSerializer();

		public static async Task<T> GetDataObjectFromAPI<T>(string apiUrl)
		{
			return await Task.Run(async () =>
			{
				try
				{
					var response = await CreateHttpClient().GetAsync(apiUrl);
					using (var stream = await response.Content.ReadAsStreamAsync())
					using (var reader = new StreamReader(stream))
					using (var json = new JsonTextReader(reader))
					{
						if (json == null)
							return default(T);

						return _serializer.Deserialize<T>(json);
					}

				}
				catch (Exception e)
				{
					return default(T);
				}
			});
		}
	}
}
