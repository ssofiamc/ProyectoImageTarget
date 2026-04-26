using UnityEngine;

public class Multitargets : MonoBehaviour
{
    [SerializeField] private GameObject startModel; //Para que una variable privada se vuelva publica
    private int modelsCount;
    private int indexCurrentModel; //Carga los modelados y su index

    void Start()
    {
        modelsCount = transform.childCount; //Le ordena al objeto image target busque los hijos que tiene ese objeto, dice cuantos modelados hay en el objeto padre
        indexCurrentModel = startModel.transform.GetSiblingIndex(); //Asigna cual es el objeto inicial de la lista para mostrar y a los demas objetos les crea un numero para que quede asignado en la lista
    }

    public void ChangeModel(int index) //Para programar los botones
    {
        transform.GetChild(indexCurrentModel).gameObject.SetActive(false); //El que le daba las ordenes en los botones de abajo de desactivar los objetos
        int newIndex = indexCurrentModel + index; //Que se vaya sumando uno en la lista de objetos

        if (newIndex < 0)//Mientras cuenta y avanza
        {
            newIndex = modelsCount - 1;
        }
        else if (newIndex > modelsCount - 1) //Activa los contadores
        {
            newIndex = 0;
        }

        GameObject newModel = transform.GetChild(newIndex).gameObject; //Sigue tomando los hijos del objeto principal y los va activado
        newModel.SetActive(true);
        indexCurrentModel = newModel.transform.GetSiblingIndex();
    }
}
