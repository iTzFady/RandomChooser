using RandomChooser.Services;

namespace RandomChooser.Tests;

public abstract class StorageTestBase : IDisposable
{
    private readonly string _originalStorageDirectory = AppSettings.StorageDirectory;

    protected string StoragePath { get; } = System.IO.Directory.CreateDirectory(
        Path.Combine(Path.GetTempPath(), $"random-chooser-tests-{Guid.NewGuid():N}")).FullName;

    protected StorageTestBase()
    {
        AppSettings.StorageDirectory = StoragePath;
    }

    public virtual void Dispose()
    {
        AppSettings.StorageDirectory = _originalStorageDirectory;

        if (System.IO.Directory.Exists(StoragePath))
        {
            System.IO.Directory.Delete(StoragePath, recursive: true);
        }
    }
}
