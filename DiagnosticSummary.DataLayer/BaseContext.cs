namespace DiagnosticsSummary.DataLayer
{
    public abstract class BaseContext<Context>: IDisposable
        where Context: IDisposable
    {
        protected readonly Context WorkingContext;

        protected BaseContext(Context workingContext)
        {
            WorkingContext = workingContext;
        }

        public void Dispose()
        {
            WorkingContext?.Dispose();
        }
    }
}
