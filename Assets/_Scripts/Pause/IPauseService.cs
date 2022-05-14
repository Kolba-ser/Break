namespace Break.Pause
{
    interface IPauseService
    {
        public bool IsPaused { get; }
        public void Register(IPauseHandler handler);
        public void Unregister(IPauseHandler handler);
        public void Pause();
        public void Unpause();
    }
}
