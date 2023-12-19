using System.Collections;
using System.Collections.Generic;

namespace Components
{
    public class Composite : Component
    {
        //A list of the currently attached components
        List<Component> components = new List<Component>();

        public override void Execute()
        {
            //Execute all opponents
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Execute();
            }
        }

        //You must add a component that inherits from IComponent

        public T AddComponent<T>(T component)
            where T : Component // The where part is a constraint
        {
            //Do not add a component if incorrect
            if (component == null)
                return default(T);

            //Add component
            components.Add(component);

            //Return said component
            return component; // optimization, do not need a component before running the function (FACTORY PATERN)
        }


        public T GetComponent<T>()
            where T : Component
        {
            //Iterate through components, and return first one
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType() == typeof(T))
                    return (T)components[i];
            }
            return default; // return null if none
        }

        public T[] GetComponents<T>()
            where T : Component
        {
            //See above, exept for all of type
            List<T> list = new List<T>();

            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType() == typeof(T))
                    list.Add((T)components[i]);
            }
            return list.ToArray();
        }

        public void RemoveComponent<T>(T component)
            where T : Component
        {
            components.Remove(component);
        }
    }
}
