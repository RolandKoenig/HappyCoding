using System.Threading;
using RolandK.Patterns;
using RolandK.Patterns.Mvvm;

namespace HappyCoding.GRpcCommunication.ClientApp.Views;

public class OptionsViewModel : PropertyChangedBase
{
    public ClientOptions Options { get; private set; }

    public DelegateCommand Command_Save { get; }

    public DelegateCommand Command_DiscardChanges { get; }

    public OptionsViewModel()
    {
        this.Options = new ClientOptions();
        this.DiscardChanges();

        this.Command_Save = new DelegateCommand(this.Save);
        this.Command_DiscardChanges = new DelegateCommand(this.DiscardChanges);
    }

    private async void Save()
    {
        await ClientOptions.SaveAsync(this.Options, CancellationToken.None);
    }

    private async void DiscardChanges()
    {
        this.Options = await ClientOptions.LoadAsync(CancellationToken.None);
        base.RaisePropertyChanged(nameof(this.Options));
    }
}
