
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.62.0.  DO NOT EDIT!
//*************************************************************************************

using XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.SideMenu.LIB.MVCS;


public abstract class TestFixtureBase : IDisposable
{
    public class TestModel : Model
    {
        public class TestStatus : Status
        {

        }

        public TestModel(string _uid) : base(_uid)
        {
        }

        protected override void setup()
        {
            Error err;
            spawnStatus<TestStatus>("test", out err);
        }
    }

    public Framework framework { get; private set; } = new Framework();
    public Logger logger { get; private set; } = new Logger();
    public Config config { get; private set; } = new Config();
    public Entry entry { get; private set; } = new Entry();
    public Model model { get; private set; } = new TestModel("test");

    public TestFixtureBase()
    {
        framework.setLogger(logger);
        framework.setConfig(config);
        framework.Initialize();

        var options = new Options();
        entry.Inject(framework, options);
        framework.setUserData("XTC.FMP.MOD.SideMenu.LIB.MVCS.Entry", entry);
        entry.StaticRegister("test", logger);
        framework.getStaticPipe().RegisterModel(model);
        framework.Setup();
    }

    public virtual void Dispose()
    {
        framework.Dismantle();
        framework.getStaticPipe().CancelModel(model);
        entry.StaticCancel("test", logger);
        framework.Release();
    }
}
