//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public static class Cache
//{
//    private static Dictionary<Collider, IHit> ihits = new Dictionary<Collider, IHit>();

//    public static IHit GetIHit(Collider collider)
//    {
//        if (!ihits.ContainsKey(collider))
//        {
//            ihits.Add(collider, collider.GetComponent<IHit>());
//        }

//        return ihits[collider];
//    }

//    private static Dictionary<Collider, _character> characters = new Dictionary<Collider, _character>();

//    public static _character GetCharacter(Collider collider)
//    {
//        if (!characters.ContainsKey(collider))
//        {
//            characters.Add(collider, collider.GetComponent<_character>());
//        }

//        return characters[collider];
//    }


//}
