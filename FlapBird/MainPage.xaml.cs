namespace FlapBird;

public partial class MainPage : ContentPage
{
   const int gravidade=30;
   const int tempoEntreFrames=25;
   bool estaMorto=false;
		public MainPage()
	{
		InitializeComponent();
	}
	void AplicaGravidade()
	{
		imgpassaro.TranslationY+=gravidade;
	}

     async Task Desenhar()
	 {
		while (!estaMorto)
		{
			AplicaGravidade();
			await Task.Delay(tempoEntreFrames);
		}
	 }
    protected override void OnAppearing()
    {
        base.OnAppearing();
		Desenhar();
    }



}

