using System;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Examples
{
    public class RoomTemplate<TDerived> where TDerived : RoomTemplate<TDerived>
    {
        protected virtual Room OnCreate(PlayableCharacter pC)
        {
            throw new NotImplementedException();
        }

        private static TDerived GetInstance()
        {
            var type = typeof(TDerived);
            return (TDerived)Activator.CreateInstance(type);
        }

        public static Room Create(PlayableCharacter pC)
        {
            return GetInstance().OnCreate(pC);
        }

        public static Identifier GetIdentifier(PlayableCharacter pC)
        {
            return Create(pC).Identifier;
        }
    }
}
