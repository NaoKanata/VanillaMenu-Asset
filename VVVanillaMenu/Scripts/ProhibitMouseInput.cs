// using UnityEngine;
// using UnityEngine.EventSystems;

// namespace VVVanilla.Menu
// {
//     public class ProhibitMouseInput : StandaloneInputModule
//     {
//         public override void Process()
//         {
//             bool usedEvent = SendUpdateEventToSelectedObject();

//             if (eventSystem.sendNavigationEvents)
//             {
//                 if (!usedEvent)
//                     usedEvent |= SendMoveEventToSelectedObject();

//                 if (!usedEvent)
//                     SendSubmitEventToSelectedObject();
//             }

//             //無効化
//             //ProcessMouseEvent();
//         }
//     }
// }