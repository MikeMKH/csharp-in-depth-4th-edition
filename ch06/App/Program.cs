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
            await PrintAndWaitWithSimpleLoop(TimeSpan.FromMilliseconds(100));
            await PrintAndWaitInLoop(TimeSpan.FromMilliseconds(50));
            await PrintAndWaitInTryFinally(TimeSpan.FromMilliseconds(100));
            await PrintAndConfigureAwait(TimeSpan.FromMilliseconds(50));
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
        
        /*
        [AsyncStateMachine (typeof(_003CPrintAndWaitWithSimpleLoop_003Ed__2))]
        private static Task PrintAndWaitWithSimpleLoop (TimeSpan delay)
        {
        	_003CPrintAndWaitWithSimpleLoop_003Ed__2 stateMachine = default(_003CPrintAndWaitWithSimpleLoop_003Ed__2);
        	stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create ();
        	stateMachine.delay = delay;
        	stateMachine._003C_003E1__state = -1;
        	stateMachine._003C_003Et__builder.Start (ref stateMachine);
        	return stateMachine._003C_003Et__builder.Task;
        }
        */
        static async Task PrintAndWaitWithSimpleLoop(TimeSpan delay)
        {
            Console.WriteLine("before delays");
            await Task.Delay(delay);
            for(int i = 0; i < 3; i++)
            {
                Console.WriteLine("between delays");
            }
            await Task.Delay(delay);
            Console.WriteLine("after delays");   
        }
        /*
        [StructLayout (LayoutKind.Auto)]
        [CompilerGenerated]
        private struct _003CPrintAndWaitWithSimpleLoop_003Ed__2 : IAsyncStateMachine
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
        					goto IL_00e7;
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
        			for (int i = 0; i < 3; i++) {
        				Console.WriteLine ("between delays");
        			}
        			awaiter = Task.Delay (delay).GetAwaiter ();
        			if (!awaiter.IsCompleted) {
        				num = (_003C_003E1__state = 1);
        				_003C_003Eu__1 = awaiter;
        				_003C_003Et__builder.AwaitUnsafeOnCompleted (ref awaiter, ref this);
        				return;
        			}
        			goto IL_00e7;
        			IL_00e7:
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
        
        /*
        [AsyncStateMachine (typeof(_003CPrintAndWaitInLoop_003Ed__3))]
	    private static Task PrintAndWaitInLoop (TimeSpan delay)
	    {
	    	_003CPrintAndWaitInLoop_003Ed__3 stateMachine = default(_003CPrintAndWaitInLoop_003Ed__3);
	    	stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create ();
	    	stateMachine.delay = delay;
	    	stateMachine._003C_003E1__state = -1;
	    	stateMachine._003C_003Et__builder.Start (ref stateMachine);
	    	return stateMachine._003C_003Et__builder.Task;
	    }
        */
        static async Task PrintAndWaitInLoop(TimeSpan delay)
        {
            Console.WriteLine("Before loop");
            for(int i = 0; i < 3; i++)
            {
                Console.WriteLine("In loop before delay");
                await Task.Delay(delay);
                Console.WriteLine("In loop after delay");
            }
            Console.WriteLine("After loop");
        }
        /*
        [StructLayout (LayoutKind.Auto)]
        [CompilerGenerated]
        private struct _003CPrintAndWaitInLoop_003Ed__3 : IAsyncStateMachine
        {
        	public int _003C_003E1__state;
        
        	public AsyncTaskMethodBuilder _003C_003Et__builder;
        
        	public TimeSpan delay;
        
        	private int _003Ci_003E5__2;
        
        	private TaskAwaiter _003C_003Eu__1;
        
        	private void MoveNext ()
        	{
        		int num = _003C_003E1__state;
        		try {
        			if (num != 0) {
        				Console.WriteLine ("Before loop");
        				_003Ci_003E5__2 = 0;
        				goto IL_00a1;
        			}
        			TaskAwaiter awaiter = _003C_003Eu__1;
        			_003C_003Eu__1 = default(TaskAwaiter);
        			num = (_003C_003E1__state = -1);
        			goto IL_0080;
        			IL_0080:
        			awaiter.GetResult ();
        			Console.WriteLine ("In loop after delay");
        			_003Ci_003E5__2++;
        			goto IL_00a1;
        			IL_00a1:
        			if (_003Ci_003E5__2 < 3) {
        				Console.WriteLine ("In loop before delay");
        				awaiter = Task.Delay (delay).GetAwaiter ();
        				if (!awaiter.IsCompleted) {
        					num = (_003C_003E1__state = 0);
        					_003C_003Eu__1 = awaiter;
        					_003C_003Et__builder.AwaitUnsafeOnCompleted (ref awaiter, ref this);
        					return;
        				}
        				goto IL_0080;
        			}
        			Console.WriteLine ("After loop");
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
        
        /*
        [AsyncStateMachine (typeof(_003CPrintAndWaitInTryFinally_003Ed__4))]
	    private static Task PrintAndWaitInTryFinally (TimeSpan delay)
	    {
	    	_003CPrintAndWaitInTryFinally_003Ed__4 stateMachine = default(_003CPrintAndWaitInTryFinally_003Ed__4);
	    	stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create ();
	    	stateMachine.delay = delay;
	    	stateMachine._003C_003E1__state = -1;
	    	stateMachine._003C_003Et__builder.Start (ref stateMachine);
	    	return stateMachine._003C_003Et__builder.Task;
	    }
        */
        static async Task PrintAndWaitInTryFinally(TimeSpan delay)
        {
            Console.WriteLine("Before try");
            await Task.Delay(delay);
            try
            {
                Console.WriteLine("In try before delay");
                await Task.Delay(delay);
                Console.WriteLine("In try after delay");
            }
            finally
            {
                Console.WriteLine("In finally");
            }
            Console.WriteLine("After try");
        }
        /*
        [StructLayout (LayoutKind.Auto)]
        [CompilerGenerated]
        private struct _003CPrintAndWaitInTryFinally_003Ed__4 : IAsyncStateMachine
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
        					goto IL_0078;
        				}
        				Console.WriteLine ("Before try");
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
        			goto IL_0078;
        			IL_0078:
        			try {
        				if (num != 1) {
        					Console.WriteLine ("In try before delay");
        					awaiter = Task.Delay (delay).GetAwaiter ();
        					if (!awaiter.IsCompleted) {
        						num = (_003C_003E1__state = 1);
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
        				Console.WriteLine ("In try after delay");
        			} finally {
        				if (num < 0) {
        					Console.WriteLine ("In finally");
        				}
        			}
        			Console.WriteLine ("After try");
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
        
        /*
        [AsyncStateMachine (typeof(_003CPrintAndConfigureAwait_003Ed__5))]
    	private static Task PrintAndConfigureAwait (TimeSpan delay)
    	{
    		_003CPrintAndConfigureAwait_003Ed__5 stateMachine = default(_003CPrintAndConfigureAwait_003Ed__5);
    		stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create ();
    		stateMachine.delay = delay;
    		stateMachine._003C_003E1__state = -1;
    		stateMachine._003C_003Et__builder.Start (ref stateMachine);
    		return stateMachine._003C_003Et__builder.Task;
    	}
        */
        static async Task PrintAndConfigureAwait(TimeSpan delay)
        {
            Console.WriteLine("Before delay without ConfigureAwait");
            await Task.Delay(delay);
            Console.WriteLine("After delay without ConfigureAwait");
            
            Console.WriteLine("Before delay with ConfigureAwait(false)");
            await Task.Delay(delay).ConfigureAwait(false);
            Console.WriteLine("After delay with ConfigureAwait(false)");
            
            Console.WriteLine("Before delay with ConfigureAwait(true)");
            await Task.Delay(delay).ConfigureAwait(true);
            Console.WriteLine("After delay with ConfigureAwait(true)");
            
            Console.WriteLine("Before delay with ContinueWith");
            await Task.Delay(delay).ContinueWith(async (_) => await Task.Delay(delay));
            Console.WriteLine("After delay with ContinueWith");
        }
        /*
        [StructLayout (LayoutKind.Auto)]
        [CompilerGenerated]
        private struct _003CPrintAndConfigureAwait_003Ed__5 : IAsyncStateMachine
        {
        	public int _003C_003E1__state;
        
        	public AsyncTaskMethodBuilder _003C_003Et__builder;
        
        	public TimeSpan delay;
        
        	private _003C_003Ec__DisplayClass5_0 _003C_003E8__1;
        
        	private TaskAwaiter _003C_003Eu__1;
        
        	private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__2;
        
        	private TaskAwaiter<Task> _003C_003Eu__3;
        
        	private void MoveNext ()
        	{
        		int num = _003C_003E1__state;
        		try {
        			TaskAwaiter awaiter3;
        			ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
        			TaskAwaiter<Task> awaiter;
        			switch (num) {
        			default:
        				_003C_003E8__1 = new _003C_003Ec__DisplayClass5_0 ();
        				_003C_003E8__1.delay = delay;
        				Console.WriteLine ("Before delay without ConfigureAwait");
        				awaiter3 = Task.Delay (_003C_003E8__1.delay).GetAwaiter ();
        				if (!awaiter3.IsCompleted) {
        					num = (_003C_003E1__state = 0);
        					_003C_003Eu__1 = awaiter3;
        					_003C_003Et__builder.AwaitUnsafeOnCompleted (ref awaiter3, ref this);
        					return;
        				}
        				goto IL_00a1;
        			case 0:
        				awaiter3 = _003C_003Eu__1;
        				_003C_003Eu__1 = default(TaskAwaiter);
        				num = (_003C_003E1__state = -1);
        				goto IL_00a1;
        			case 1:
        				awaiter2 = _003C_003Eu__2;
        				_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
        				num = (_003C_003E1__state = -1);
        				goto IL_0123;
        			case 2:
        				awaiter2 = _003C_003Eu__2;
        				_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
        				num = (_003C_003E1__state = -1);
        				goto IL_01a5;
        			case 3:
        				{
        					awaiter = _003C_003Eu__3;
        					_003C_003Eu__3 = default(TaskAwaiter<Task>);
        					num = (_003C_003E1__state = -1);
        					break;
        				}
        				IL_0123:
        				awaiter2.GetResult ();
        				Console.WriteLine ("After delay with ConfigureAwait(false)");
        				Console.WriteLine ("Before delay with ConfigureAwait(true)");
        				awaiter2 = Task.Delay (_003C_003E8__1.delay).ConfigureAwait (continueOnCapturedContext: true).GetAwaiter ();
        				if (!awaiter2.IsCompleted) {
        					num = (_003C_003E1__state = 2);
        					_003C_003Eu__2 = awaiter2;
        					_003C_003Et__builder.AwaitUnsafeOnCompleted (ref awaiter2, ref this);
        					return;
        				}
        				goto IL_01a5;
        				IL_00a1:
        				awaiter3.GetResult ();
        				Console.WriteLine ("After delay without ConfigureAwait");
        				Console.WriteLine ("Before delay with ConfigureAwait(false)");
        				awaiter2 = Task.Delay (_003C_003E8__1.delay).ConfigureAwait (continueOnCapturedContext: false).GetAwaiter ();
        				if (!awaiter2.IsCompleted) {
        					num = (_003C_003E1__state = 1);
        					_003C_003Eu__2 = awaiter2;
        					_003C_003Et__builder.AwaitUnsafeOnCompleted (ref awaiter2, ref this);
        					return;
        				}
        				goto IL_0123;
        				IL_01a5:
        				awaiter2.GetResult ();
        				Console.WriteLine ("After delay with ConfigureAwait(true)");
        				Console.WriteLine ("Before delay with ContinueWith");
        				awaiter = Task.Delay (_003C_003E8__1.delay).ContinueWith (delegate {
        					_003C_003Ec__DisplayClass5_0._003C_003CPrintAndConfigureAwait_003Eb__0_003Ed stateMachine = default(_003C_003Ec__DisplayClass5_0._003C_003CPrintAndConfigureAwait_003Eb__0_003Ed);
        					stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create ();
        					stateMachine._003C_003E4__this = _003C_003E8__1;
        					stateMachine._003C_003E1__state = -1;
        					stateMachine._003C_003Et__builder.Start (ref stateMachine);
        					return stateMachine._003C_003Et__builder.Task;
        				}).GetAwaiter ();
        				if (!awaiter.IsCompleted) {
        					num = (_003C_003E1__state = 3);
        					_003C_003Eu__3 = awaiter;
        					_003C_003Et__builder.AwaitUnsafeOnCompleted (ref awaiter, ref this);
        					return;
        				}
        				break;
        			}
        			awaiter.GetResult ();
        			Console.WriteLine ("After delay with ContinueWith");
        		} catch (Exception exception) {
        			_003C_003E1__state = -2;
        			_003C_003E8__1 = null;
        			_003C_003Et__builder.SetException (exception);
        			return;
        		}
        		_003C_003E1__state = -2;
        		_003C_003E8__1 = null;
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
}
