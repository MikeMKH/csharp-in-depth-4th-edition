using System;
using System.Threading.Tasks;

namespace App
{
    class Program
    {
        /*
        private static void _003CMain_003E (string[] args)
        {
        	Main (args).GetAwaiter ().GetResult ();
        }
        */
        static async Task Main(string[] args)
        {
            await PrintAndWait(TimeSpan.FromMilliseconds(100));
        }
        
        /*
        [AsyncStateMachine (typeof(_003CPrintAndWait_003Ed__1))]
        private static Task PrintAndWait (TimeSpan delay)
        {
        	_003CPrintAndWait_003Ed__1 stateMachine = default(_003CPrintAndWait_003Ed__1);
        	stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create ();
        	stateMachine.delay = delay;
        	stateMachine._003C_003E1__state = -1;
        	stateMachine._003C_003Et__builder.Start (ref stateMachine);
        	return stateMachine._003C_003Et__builder.Task;
        }
        */
        static async Task PrintAndWait(TimeSpan delay)
        {
            Console.WriteLine("before delays");
            await Task.Delay(delay);
            Console.WriteLine("between delays");
            await Task.Delay(delay);
            Console.WriteLine("after delays");
        }
    }
    
    /*
    [StructLayout (LayoutKind.Auto)]
    [CompilerGenerated]
    private struct _003CPrintAndWait_003Ed__1 : IAsyncStateMachine
    {
    	public int _003C_003E1__state;
    
    	public AsyncTaskMethodBuilder _003C_003Et__builder;
    
    	public TimeSpan delay;
    
    	private TaskAwaiter _003C_003Eu__1;
    
    	private void MoveNext ()
    	{
    		int num = _003C_003E1__state;
    		try {
    			TaskAwaiter awaiter;
    			if (num != 0) {
    				if (num == 1) {
    					awaiter = _003C_003Eu__1;
    					_003C_003Eu__1 = default(TaskAwaiter);
    					num = (_003C_003E1__state = -1);
    					goto IL_00db;
    				}
    				Console.WriteLine ("before delays");
    				awaiter = Task.Delay (delay).GetAwaiter ();
    				if (!awaiter.IsCompleted) {
    					num = (_003C_003E1__state = 0);
    					_003C_003Eu__1 = awaiter;
    					_003C_003Et__builder.AwaitUnsafeOnCompleted (ref awaiter, ref this);
    					return;
    				}
    			} else {
    				awaiter = _003C_003Eu__1;
    				_003C_003Eu__1 = default(TaskAwaiter);
    				num = (_003C_003E1__state = -1);
    			}
    			awaiter.GetResult ();
    			Console.WriteLine ("between delays");
    			awaiter = Task.Delay (delay).GetAwaiter ();
    			if (!awaiter.IsCompleted) {
    				num = (_003C_003E1__state = 1);
    				_003C_003Eu__1 = awaiter;
    				_003C_003Et__builder.AwaitUnsafeOnCompleted (ref awaiter, ref this);
    				return;
    			}
    			goto IL_00db;
    			IL_00db:
    			awaiter.GetResult ();
    			Console.WriteLine ("after delays");
    		} catch (Exception exception) {
    			_003C_003E1__state = -2;
    			_003C_003Et__builder.SetException (exception);
    			return;
    		}
    		_003C_003E1__state = -2;
    		_003C_003Et__builder.SetResult ();
    	}
    
    	void IAsyncStateMachine.MoveNext ()
    	{
    		//ILSpy generated this explicit interface implementation from .override directive in MoveNext
    		this.MoveNext ();
    	}
    
    	[DebuggerHidden]
    	private void SetStateMachine (IAsyncStateMachine stateMachine)
    	{
    		_003C_003Et__builder.SetStateMachine (stateMachine);
    	}
    
    	void IAsyncStateMachine.SetStateMachine (IAsyncStateMachine stateMachine)
    	{
    		//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
    		this.SetStateMachine (stateMachine);
    	}
    }
    */
}
