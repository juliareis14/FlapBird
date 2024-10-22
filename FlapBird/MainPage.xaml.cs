namespace FlapBird;

public partial class MainPage : ContentPage
{
	const int gravidade = 30;
	const int tempoEntreFrames = 25;
	bool estaMorto = false;
	double larguraJanela = 0;
	double alturaJanela = 0;
	int velocidade = 20;
	const int maxTempoPulando = 3;
	int tempoPulando = 0;
	bool estaPulando = false;
	const int forcaPulo = 60;
	const int aberturaMinima = 200;
	int score=0;


	public MainPage()
	{
		InitializeComponent();
	}
	void AplicaPulo()
	{
		imgpassaro.TranslationY -= forcaPulo;
		tempoPulando++;
		if (tempoPulando >= maxTempoPulando)
		{
			estaPulando = false;
			tempoPulando = 0;
		}

	}
	void AplicaGravidade()
	{
		imgpassaro.TranslationY += gravidade;
	}

	async Task Desenhar()
	{
		while (!estaMorto)
		{
			if (estaPulando)
				AplicaPulo();
			else
				AplicaGravidade();

			GerenciaCanos();
			if (VerificaColisao())
			{
				estaMorto = true;
				FrameGameOver.IsVisible = true;
				break;
			}
			await Task.Delay(tempoEntreFrames);
		}
	}



	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		larguraJanela = w;
		alturaJanela = h;
	}

	void GerenciaCanos()
	{
		CanoDeCima.TranslationX -= velocidade;
		CanoDeBaixo.TranslationX -= velocidade;
		if (CanoDeBaixo.TranslationX <= -larguraJanela)
		{
			CanoDeBaixo.TranslationX = 4;
			CanoDeCima.TranslationX = 4;
			var alturaMax = -100;
			var alturaMin = -CanoDeBaixo.HeightRequest;
			CanoDeCima.TranslationY = Random.Shared.Next((int)alturaMin, (int)alturaMax);
			CanoDeBaixo.TranslationY = CanoDeCima.TranslationY + aberturaMinima + CanoDeBaixo.HeightRequest;

			score++;
			labelScore.Text = "Canos:" + score.ToString("D3");

		}

	}
	void OnGameOverClicked(object s, TappedEventArgs a)
	{
		Inicializar();
		FrameGameOver.IsVisible = false;
		Desenhar();
	}
	void Inicializar()
	{
		estaMorto = false;
		CanoDeCima.TranslationX = -larguraJanela;
		CanoDeCima.TranslationX = -larguraJanela;
		imgpassaro.TranslationX = 0;
		imgpassaro.TranslationY = 0;
		score = 0;
		GerenciaCanos();
	}

	bool VerificaColisao()
	{
		if (!estaMorto)
		{
			if (VerificaColisaoTeto() ||
			    VerificaColisaoChao() ||
			VerificaColisaoCanoCima() ||
			VerificaColisaoCanoBaixo())
		
				return true;

			
		}
				return false;
	}
	
	bool VerificaColisaoTeto()
	{
		var minY = -alturaJanela / 2;
		if (imgpassaro.TranslationY <= minY)
			return true;
		else
			return false;
	}
	bool VerificaColisaoChao()
	{
		var maxY = alturaJanela / 2;
		if (imgpassaro.TranslationY >= maxY)
			return true;
		else
			return false;

	}

	void OnGridClicked(object s, TappedEventArgs a)
	{
		estaPulando = true;
	}



 
 bool VerificaColisaoCanoCima()
	{
		var posHimgpassaro = (larguraJanela / 2) - (imgpassaro.WidthRequest / 2);
		var posVimgpassaro = (alturaJanela / 2) - (imgpassaro.HeightRequest / 2) + imgpassaro.TranslationY;
		if (posHimgpassaro >= Math.Abs(CanoDeCima.TranslationX) - CanoDeCima.WidthRequest &&
		 posHimgpassaro <= Math.Abs(CanoDeCima.TranslationX) + CanoDeCima.WidthRequest &&
		 posVimgpassaro <= CanoDeCima.HeightRequest + CanoDeCima.TranslationY)
		 {
			return true;
		 }
		 else 
		 {
			return false;
		 }
	}

	bool VerificaColisaoCanoBaixo()
	{
		var posHimgpassaro = (larguraJanela / 2) - (imgpassaro.WidthRequest / 2);
		var posVimgpassaro = (alturaJanela / 2) - (imgpassaro.HeightRequest / 2) + imgpassaro.TranslationY;
		if (posHimgpassaro >= Math.Abs(CanoDeBaixo.TranslationX) - CanoDeBaixo.WidthRequest &&
		 posHimgpassaro <= Math.Abs(CanoDeBaixo.TranslationX) + CanoDeBaixo.WidthRequest &&
		 posVimgpassaro <= CanoDeBaixo.HeightRequest + CanoDeBaixo.TranslationY)
		 {
			return true;
		 }
		 else 
		 {
			return false;
		 }
	}
	
}