using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TrocaDeMusicas : MonoBehaviour
{
    public Button botaoProximaMusica;
    public Button botaoPausa;
    public AudioSource audioSource;
    public TextMeshProUGUI nomeMusicaText;
    public List<MusicInfo> musicas = new List<MusicInfo>();

    private int indiceMusicaAtual = 0;
    private bool pausado = false;

    private void Start()
    {
        // Associe os métodos aos eventos de clique dos botões
        botaoProximaMusica.onClick.AddListener(TrocarMusica);
        botaoPausa.onClick.AddListener(PausarOuRetomarMusica);

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

    private void PausarOuRetomarMusica()
    {
        if (pausado)
        {
            // Se já estiver pausado, retome a reprodução
            audioSource.UnPause();
            pausado = false;
        }
        else
        {
            // Caso contrário, pause a reprodução
            audioSource.Pause();
            pausado = true;
        }
    }
}

[System.Serializable]
public class MusicInfo
{
    public string nome;
    public AudioClip audioClip;
}
