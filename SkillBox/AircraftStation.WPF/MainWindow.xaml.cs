using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace AircraftStation.WPF
{
	public partial class MainWindow : Window
	{
		private readonly IStation station;

		public MainWindow()
		{
			InitializeComponent();
			station = new Station(5);
			station.OnStationStateChange += OnStationStateChange;
		}

		private void OnStationStateChange(object sender, EventArgs e)
		{
			var idInput = AircraftIdInput.Text;
			SetEnabledTakeAircraftButton(idInput);
			SendNextAircraftButton.IsEnabled = station.AnyAircraftsOnStation();
			AircraftIdInput.IsEnabled = station.CheckFreeSlots(out _);
		}

		private void AircraftIdInput_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var id = AircraftIdInput.Text;
			SetEnabledTakeAircraftButton(id);
		}

		private void SetEnabledTakeAircraftButton(string id)
		{
			TakeAircraftButton.IsEnabled = !string.IsNullOrEmpty(id)
			                               && station.CheckFreeSlots(out _)
			                               && !station.CheckOnAircraftOnStation(id);
		}

		private void TakeAircraftButton_OnClick(object sender, RoutedEventArgs e)
		{
			var id = AircraftIdInput.Text;
			if (string.IsNullOrEmpty(id) || station.CheckOnAircraftOnStation(id) || !station.CheckFreeSlots(out _))
				return;
			var isTakeSuccess = station.TryTakeAircraft(id);
			AppendConsoleNewLine(isTakeSuccess
				? $"Принят самолёт {id}"
				: $"Не удалось принять самолёт {id}!!!");
		}

		private void SendNextAircraftButton_OnClick(object sender, RoutedEventArgs e)
		{
			if (!station.AnyAircraftsOnStation())
				return;
			var isSendSuccess = station.TrySendAircraft(out var id);
			AppendConsoleNewLine(isSendSuccess
			? $"Отправлен самолёт {id}"
			: $"Не удалось отправить самолёт!!!");
		}

		private void AppendConsoleNewLine(string text)
		{
			var consoleText = new TextRange(OutputConsoleBox.Document.ContentStart, OutputConsoleBox.Document.ContentEnd).Text;
			if (string.IsNullOrEmpty(consoleText))
				OutputConsoleBox.AppendText(text);
			else
				OutputConsoleBox.AppendText(Environment.NewLine + text);
			OutputConsoleBox.ScrollToEnd();
		}
	}
}
