using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void GuardarPartida()
    {
        string path = Application.persistentDataPath + "/jugador.txt";

        FileStream stream = new FileStream(path, FileMode.Create);

        PerfilJugador perfil = new PerfilJugador();

        BinaryFormatter formatter = new BinaryFormatter();

        formatter.Serialize(stream, perfil);

        stream.Close();
    }

    public static PerfilJugador CargarPartida()
    {
        string path = Application.persistentDataPath + "/jugador.txt";

        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);

            BinaryFormatter formatter = new BinaryFormatter();

            PerfilJugador perfil = formatter.Deserialize(stream) as PerfilJugador;

            stream.Close();

            return perfil;
        }
        else
        {

            return null;
        }
    }

}
