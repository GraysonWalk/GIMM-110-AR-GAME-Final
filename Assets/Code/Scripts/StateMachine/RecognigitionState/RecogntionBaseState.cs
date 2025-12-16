public abstract class RecognitionBaseState : IState
{
    #region Constructor

    
    /// <summary>
    /// Constructor to enforce parameters for all states
    /// </summary>
    protected RecognitionBaseState()
    {
    }

    #endregion

    #region Methods

    public virtual void OnEnter()
    {
        // noop
    }

    public virtual void Update()
    {
        // noop
    }

    public virtual void FixedUpdate()
    {
        // noop
    }

    public virtual void OnExit()
    {
        // noop
    }

    #endregion
}