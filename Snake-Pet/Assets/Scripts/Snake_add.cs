using UnityEngine;

public class Snake_add : MonoBehaviour
{
    public GameObject cuerpoPrefab;  // Prefab de la parte del cuerpo a agregar
    public Transform cuerpoParent;   // Transform padre donde se agregan las partes del cuerpo
    private Transform[] cuerpoSlots;  // Array de transformaciones de los slots de cuerpo
    private float slotSpacingX = 20f; // Espaciado en X entre los slots de cuerpo

    private void Start()
    {
        // Configurar los slots de cuerpo inicialmente
        InitializeBodySlots();
    }

    public void InitializeBodySlots()
    {
        // Crear un array de Transform con tantos elementos como necesites
        cuerpoSlots = new Transform[100]; // Por ejemplo, inicialmente 100 slots

        // Crear los slots y configurarlos como hijos del cuerpoParent
        for (int i = 0; i < cuerpoSlots.Length; i++)
        {
            GameObject slot = new GameObject("CuerpoSlot" + i); // Nombre del GameObject del slot
            slot.transform.SetParent(cuerpoParent);  // Asignar como hijo del cuerpoParent

            // Ajustar posición local del slot en relación con el padre Serpiente
            slot.transform.localPosition = new Vector3(110f + i * slotSpacingX, 0f, 0f);

            cuerpoSlots[i] = slot.transform;  // Asignar al array de cuerpoSlots
        }
    }

    public void AddBodyPart()
    {
        // Verificar si hay slots disponibles para el cuerpo
        int nextSlotIndex = FindNextAvailableSlot();
        if (nextSlotIndex != -1)
        {
            // Clonar el prefab de la parte del cuerpo
            GameObject newBodyPart = Instantiate(cuerpoPrefab, cuerpoSlots[nextSlotIndex]);

            // Obtener la posición actual de la cola
            Transform cola = cuerpoParent.Find("Cola");
            if (cola != null)
            {
                // Actualizar la posición de la cola
                DesplazarCola(slotSpacingX);
            }
            else
            {
                Debug.LogWarning("No se encontró el gameObject 'Cola'. Asegúrate de que esté configurado correctamente.");
            }
        }
        else
        {
            Debug.LogWarning("No hay más slots disponibles para el cuerpo.");
        }
    }

    // Método para encontrar el siguiente slot disponible para el cuerpo
    public int FindNextAvailableSlot()
    {
        for (int i = 0; i < cuerpoSlots.Length; i++)
        {
            if (cuerpoSlots[i] == null)
            {
                // Si el slot está destruido, continuar buscando el siguiente
                continue;
            }

            if (cuerpoSlots[i].childCount == 0)  // Verificar si el slot no tiene hijos
            {
                return i;  // Devolver el índice del slot disponible
            }
        }
        return -1;  // No se encontró ningún slot disponible
    }

    // Método para desplazar la cola a la derecha
    public void DesplazarCola(float offsetX)
    {
        // Buscar el gameObject de la cola
        Transform cola = cuerpoParent.Find("Cola");
        if (cola != null)
        {
            // Desplazar la cola
            Vector3 newPosition = cola.localPosition;
            newPosition.x += offsetX;
            cola.localPosition = newPosition;
        }
        else
        {
            Debug.LogWarning("No se encontró el gameObject 'Cola'. Asegúrate de que esté configurado correctamente.");
        }
    }
}
