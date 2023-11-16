
namespace Super_Mario
{
    public class CustomTimer
    {
        private double currentTime = 0.0;
        public void ResetAndStart(double delay)
        {
            currentTime = delay;
        }
        public bool IsDone()
        {
            return currentTime <= 0.0;
        }
        public void Update(double deltaTime)
        {
            currentTime -= deltaTime;
        }
    }
}
