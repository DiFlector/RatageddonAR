using UnityEngine;

namespace Enemy
{
    public class TransformArrayAdder : MonoBehaviour
    {
        // Массив Transform, который мы хотим редактировать в инспекторе
        public Transform[] transforms;

        // Вспомогательная переменная для хранения временного Transform, который мы хотим добавить
        public Transform newTransform;

        // Функция для добавления нового Transform в массив
        public void AddTransform()
        {
            // Создаем новый массив на один элемент больше
            Transform[] newTransformArray = new Transform[transforms.Length + 1];
        
            // Копируем старые элементы из массива в новый массив
            for (int i = 0; i < transforms.Length; i++)
            {
                newTransformArray[i] = transforms[i];
            }

            // Добавляем новый Transform в конец нового массива
            newTransformArray[newTransformArray.Length - 1] = newTransform;

            // Заменяем старый массив новым
            transforms = newTransformArray;
        }
    }
}