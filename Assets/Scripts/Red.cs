using System;
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
        public String nombre;
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
        botonLeer.clicked += LeerTextoPlano;

        Button botonEnviar = root.Q<Button>("BotonEnviar");
        botonEnviar.clicked += EnviarDatosJSON;
    }

    private void EnviarDatosJSON()
    {
        StartCoroutine(SubirDatosJSON());
    }

    private IEnumerator SubirDatosJSON()
    {
        DatosUsuario datos;
        datos.nombre = tfNombre.value;

        datos.puntos = int.Parse(tfPuntos.value.ToString());
        String datosJSON = JsonUtility.ToJson(datos);

        UnityWebRequest request = UnityWebRequest.Post("http://192.168.2.10:3000/unity/recibeJSON", datosJSON, "application/json");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            tfResultado.value = datosJSON + "\nEnviado correctamente\n\n";

            String respuesta = request.downloadHandler.text;
            tfResultado.value += "Respuesta:\n" + respuesta;

            DatosUsuario usuario = JsonUtility.FromJson<DatosUsuario>(respuesta);
            tfResultado.value += "\n\nNombre: " + usuario.nombre + "\nPuntos: " + usuario.puntos;
        }
        else
        {
            tfResultado.value = "Error de conexión: " + request.responseCode.ToString();
        }

        request.Dispose();
    }

    private void LeerTextoPlano()
    {
        StartCoroutine(DescargarTextoPlano());
    }

    private IEnumerator DescargarTextoPlano()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://192.168.2.10:3000/");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            String textoPlano = request.downloadHandler.text;
            tfResultado.value = textoPlano;
        }
        else
        {
            tfResultado.value = "Error de conexión: " + request.responseCode.ToString();
        }

        request.Dispose();
    }
}
