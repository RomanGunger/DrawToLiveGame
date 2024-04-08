using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OwnGameDevUtils
{
    public class DevUtils : MonoBehaviour
    {

        // Mouse Position
        public static Vector3 GetMouseWorldPosition(Camera camera)
        {
            Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, camera);
            vec.z = 0f;
            return vec;
        }

        public static Vector3 GetMouseWorldPositionWithZ(Camera camera)
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, camera);
        }

        public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }

        public static bool MouseRect(RectTransform rect, Vector3 mousePos)
        {
            Vector2 localMousePosition = rect.InverseTransformPoint(mousePos);

            if (rect.rect.Contains(localMousePosition))
                return true;
            else
                return false;

        }

        // Touch Position
        public static Vector3 GetTouchWorldPosition(Camera camera, Vector2 touchPos)
        {
            Vector3 vec = GetTouchWorldPositionWithZ(touchPos, camera);
            vec.z = 0f;
            return vec;
        }

        public static Vector3 GetTouchWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }

        public static Vector3 GetTouchWorldPositionWithZ(Camera camera)
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, camera);
        }

        public static List<Vector3> UnitPos(int unitsCount
            , Collider unitPlacement
            , float offsetZ, float offsetX, float spasing
            , Vector3 sizeOfUnit)
        {
            List<Vector3> positions = new List<Vector3>();

            Bounds bounds;
            bounds = unitPlacement.bounds;

            float overal = 0;
            Vector3 finalPosition;
            Vector3 spaseBetweenRows = new Vector3(0, 0, 0);

            for (int i = 0; i < unitsCount; i++)
            {
                var pos = new Vector3(overal, 0, 0);
                overal += spasing;

                var offsetVector = new Vector3(offsetX + sizeOfUnit.x / 2, 1, bounds.size.z - offsetZ);

                finalPosition = bounds.min + offsetVector + pos + spaseBetweenRows;

                if (finalPosition.x >= bounds.max.x - offsetX)
                {
                    overal = 0;
                    pos = new Vector3(overal, 0, 0);

                    spaseBetweenRows.z -= spasing;
                    offsetVector = new Vector3(offsetX + sizeOfUnit.x / 2, 1, bounds.size.z - offsetZ);
                    finalPosition = bounds.min + offsetVector + pos + spaseBetweenRows;
                    overal += spasing;
                }

                positions.Add(finalPosition);
            }

            return positions;
        }
    }
}

