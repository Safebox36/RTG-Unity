namespace rtg.api.util
{
    /**
     *
     * @author Zeno410
     */
    public abstract class Acceptor<Type>
    {
        public abstract void accept(Type accepted);

        public class Ignorer<IgnoredType> : Acceptor<IgnoredType>
        {
            public override void accept(IgnoredType ignored) { }
        }

        public class OneShotRedirector<RedirectedType> : Acceptor<RedirectedType>
        {
            private Acceptor<RedirectedType> target;
            public override void accept(RedirectedType redirected)
            {
                target.accept(redirected);
                target = null;
            }
            public void redirectTo(Acceptor<RedirectedType> newTarget)
            {
                target = newTarget;
            }
        }
    }
}