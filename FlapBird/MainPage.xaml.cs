namespace FlapBird;

public partial class MainPage : ContentPage
{
   const int gravidade=30;
   const int tempoEntreFrames=25;
   bool estaMorto=false;
   double larguraJanela=0;
   double alturaJanela=0;
   int velocidade=20;


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
			GerenciaCanos();
		}
	 }
   

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		larguraJanela=w;
		alturaJanela=h;
	}

      void GerenciaCanos()
	{
		CanoDeCima.TranslationX-=velocidade;
		CanoDeBaixo.TranslationX-=velocidade;
		if(CanoDeBaixo.TranslationX<=-larguraJanela)
	 {
		CanoDeBaixo.TranslationX=0;
		CanoDeCima.TranslationX=0;

     }
  
	}
	void OnGameOverClicked(object s, TappedEventArgs a)
    {
		frameGameOver.IsVisible=false;
		Inicializar();
		Desenhar();
	}
	void Inicializar()
	{
		estaMorto=false;
	    imgpassaro.TranslationY=0;
	}

}

