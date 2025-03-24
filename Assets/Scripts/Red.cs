
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class Red : MonoBehaviour
{
    private TextField tfResultado;
    private TextField tfNombre;
    private IntegerField tfPuntos;

    public struct DatosUsuario
    {
        public string nombre;
        public int puntos;
    }


    void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        tfResultado = root.Q<TextField>("Resultado");
        print(tfResultado.value);
        tfNombre = root.Q<TextField>("Nombre");
        tfPuntos = root.Q<IntegerField>("Puntos");
        print(tfPuntos.value);

        Button botonLeer = root.Q<Button>("BotonLeer");
        //botonLeer.clicked += LeerTextoPlano;

        Button botonEnviar = root.Q<Button>("BotonEnviar");
        //botonEnviar.clicked += EnviarDatosJSON;
    }
}
    