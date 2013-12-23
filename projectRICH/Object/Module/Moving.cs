using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.Object.Module
{
    public class Moving : IModule
    {
        private Entity.Vector currentPosition;
        private Entity.Vector currentVelocity;
        private float maxVelocity = 0.00001f;
        private Entity.Vector targetPosition;
        private long lastPositionUpdateTime;

        public Entity.Vector CurrentVelocity
        {
            get { return currentVelocity; }
        }

        [ModuleCommand("GetCurrentPosition")]
        public Entity.Vector GetCurrentPosition(IGameObject owner)
        {
            if (!currentVelocity.IsZero())
            {
                var now = GlobalClock.Now;

                if (now != lastPositionUpdateTime)
                {
                    var elapsed = now - lastPositionUpdateTime;
                    var nextPosition = currentPosition.Add(currentVelocity.Multiply(elapsed));

                    var currentToTarget = targetPosition.Diff(currentPosition);
                    var nextToTarget = targetPosition.Diff(nextPosition);

                    if (currentToTarget.Dot(nextToTarget) < 0)
                    {
                        currentPosition = targetPosition;
                        Stop(owner);
                    }
                    else
                    {
                        currentPosition = nextPosition;
                    }
                }

                lastPositionUpdateTime = now;
            }
            return currentPosition; 
        }

        [ModuleCommand("Stop")]
        public void Stop(IGameObject owner)
        {
            currentVelocity.Reset();
        }

        [ModuleCommand("WalkTo")]
        public void WalkTo(IGameObject owner, Entity.Vector targetPosition)
        {
            this.targetPosition = targetPosition;

            var diff = targetPosition.Diff(GetCurrentPosition(owner));
            diff.Normalize();
            lastPositionUpdateTime = GlobalClock.Now;
            currentVelocity = diff.Multiply(maxVelocity);
        }

        [ModuleCommand("SetPosition")]
        public void SetPosition(IGameObject owner, Entity.Vector targetPosition)
        {
            currentPosition = targetPosition;
            lastPositionUpdateTime = GlobalClock.Now;

            currentVelocity.Reset();

        }

        public void ReadFrom(System.IO.Stream strm)
        {
            throw new NotImplementedException();
        }

        public void WriteTo(System.IO.Stream strm)
        {
            throw new NotImplementedException();
        }
    }
}
