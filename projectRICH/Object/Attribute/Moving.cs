using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectRICH.Object.Attribute
{
    [Attribute.AttributeName("Moving")]
    class Moving : Attribute.IAttribute, ISerializable
    {
        private Entity.Vector currentPosition;
        private Entity.Vector currentVelocity;
        private Entity.Vector maxVelocity;
        private Entity.Vector targetPosition;

        private long lastUpdateTime;

        public Entity.Vector CurrentPosition
        {
            get { return currentPosition; }
        }

        public Entity.Vector CurrentVelocity
        {
            get { return currentVelocity; }
        }

        public void OnUpdate(long currentTime)
        {
            var elapsed = currentTime - lastUpdateTime;
            var nextPosition = currentPosition.Add(currentVelocity.Multiply(elapsed));

            var currentToTarget = targetPosition.Diff(currentPosition);
            var nextToTarget = targetPosition.Diff(nextPosition);

            if (currentToTarget.Dot(nextToTarget) < 0)
            {
                currentPosition = targetPosition;
                Stop();
            }
            else
            {
                currentPosition = nextPosition;
            }

            lastUpdateTime = currentTime;
        }

        [AttributeCommand("Stop")]
        public void Stop()
        {
            currentVelocity.Reset();
        }

        [AttributeCommand("WalkTo")]
        public void WalkTo(Entity.Vector targetPosition)
        {
            this.targetPosition = targetPosition;

            var diff = CurrentPosition.Diff(targetPosition);
            diff.Normalize();

            currentVelocity = diff.Multiply(maxVelocity);
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
