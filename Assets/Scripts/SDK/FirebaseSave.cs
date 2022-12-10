// using System;
// using System.Globalization;
// using Cysharp.Threading.Tasks;
// using Firebase.Database;
// using Firebase.Extensions;
// using UnityEngine;
//
// namespace SDK
// {
//     public class FirebaseSave : MonoBehaviour
//     {
//         public async UniTask LoadData()
//         {
//             await FirebaseDatabase.DefaultInstance
//                 .GetReference("Money")
//                 .GetValueAsync().ContinueWithOnMainThread(task => {
//                     if (task.IsFaulted) {
//                         // Handle the error...
//                     }
//                     else if (task.IsCompleted) {
//                         DataSnapshot snapshot = task.Result;
//                         var moneyLoadFirebase = snapshot.Value;
//                         GameManager.Instance.Money = Convert.ToSingle(moneyLoadFirebase);
//                     }
//                 });
//         }
//
//         public void SaveData()
//         {
//             DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
//             reference.Child("Money").SetValueAsync((double)GameManager.Instance.Money);
//         }
//     }
// }
