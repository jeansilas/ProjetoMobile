using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TrocaDeMusicas : MonoBehaviour
{
    public Button botaoProximaMusica;
    public AudioSource audioSource;
    public TextMeshProUGUI nomeMusicaText;
    public List<MusicInfo> musicas = new List<MusicInfo>();

    private int indiceMusicaAtual = 0;

    private void Start()
    {
        // Associe o método TrocarMusica ao clique do botão
        botaoProximaMusica.onClick.AddListener(TrocarMusica);

        // Verifica se há músicas na lista
        if (musicas.Count > 0)
        {
            // Inicialize a música atual
            audioSource.clip = musicas[indiceMusicaAtual].audioClip;
            audioSource.Play();

            // Exiba o nome da música atual
            nomeMusicaText.text = musicas[indiceMusicaAtual].nome;
        }
        else
        {
            Debug.LogWarning("A lista de músicas está vazia.");
        }
    }

    private void TrocarMusica()
    {
        // Verifica se há músicas na lista
        if (musicas.Count > 0)
        {
            // Avance para a próxima música
            indiceMusicaAtual++;
            if (indiceMusicaAtual >= musicas.Count)
            {
                indiceMusicaAtual = 0;
            }

            // Atualize o AudioClip da AudioSource com a nova música
            audioSource.clip = musicas[indiceMusicaAtual].audioClip;
            audioSource.Play();

            // Atualize o texto com o nome da música atual
            nomeMusicaText.text = musicas[indiceMusicaAtual].nome;
        }
    }
}

[System.Serializable]
public class MusicInfo
{
    public string nome;
    public AudioClip audioClip;
}
