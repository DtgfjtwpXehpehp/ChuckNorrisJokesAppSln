using System.ComponentModel;
using ChuckNorrisJokesApp.ViewModels;

namespace ChuckNorrisJokesApp.Views;

public partial class JokesView : ContentPage
{
    private static readonly string[] LoadingMessages =
    {
        "Rehearsing the punchline...",
        "Warming up the crowd...",
        "Consulting the comedy gods...",
        "Waiting for the laugh track..."
    };

    private readonly JokesViewModel _vm;
    private CancellationTokenSource? _animationCts;

    public JokesView(JokesViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.PropertyChanged += OnViewModelPropertyChanged;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _vm.PropertyChanged -= OnViewModelPropertyChanged;
        StopMaskAnimation();
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(JokesViewModel.IsLoading))
            return;

        if (_vm.IsLoading)
            StartMaskAnimation();
        else
            StopMaskAnimation();
    }

    private void StartMaskAnimation()
    {
        StopMaskAnimation();
        _animationCts = new CancellationTokenSource();
        _ = RunMaskAnimationAsync(_animationCts.Token);
    }

    private void StopMaskAnimation()
    {
        _animationCts?.Cancel();
        _animationCts = null;

        MasksImage.CancelAnimations();
        MasksImage.Rotation = 0;
        MasksImage.ScaleX = 1;
        MasksImage.ScaleY = 1;
    }

    private async Task RunMaskAnimationAsync(CancellationToken token)
    {
        int message = 0;

        while (!token.IsCancellationRequested)
        {
            LoadingLabel.Text = LoadingMessages[message++ % LoadingMessages.Length];

            // 1. Tilt toward the laughing mask, with a little hop in size
            await Task.WhenAll(
                MasksImage.RotateToAsync(-14, 400, Easing.SinInOut),
                MasksImage.ScaleYToAsync(1.1, 400, Easing.SinInOut));
            if (token.IsCancellationRequested) return;

            // 2. Swing over to the crying mask
            await Task.WhenAll(
                MasksImage.RotateToAsync(14, 700, Easing.SinInOut),
                MasksImage.ScaleYToAsync(0.95, 700, Easing.SinInOut));
            if (token.IsCancellationRequested) return;

            // 3. Level out and flip, so the masks swap places
            await Task.WhenAll(
                MasksImage.RotateToAsync(0, 500, Easing.SinInOut),
                MasksImage.ScaleYToAsync(1, 500, Easing.SinInOut),
                MasksImage.ScaleXToAsync(MasksImage.ScaleX > 0 ? -1 : 1, 500, Easing.CubicInOut));
        }
    }
}