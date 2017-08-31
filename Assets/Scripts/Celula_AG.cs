using UnityEngine;

public class Celula_AG : MonoBehaviour {

    public static int population = 10;
    public static int generations = 10;

	
    public Vector3 GetTarget()
    {
        Vector3 virusperto = VirusPerto();
        Vector3[] alvos = new Vector3[population];
        float[] fitness = new float[population];

        // gera população inicial
        for (int i = 0; i < population; i++)
        {
            alvos[i] = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), -10);
        }
        for (int g = 0; g < generations; g++)
        {
            Vector3[] novosAlvos = new Vector3[population];
            // calcula fitness (distancia entre o alvo e o virus mais perto)
            for (int i = 0; i < population; i++) // pra cada alvo
            {
                fitness[i] = Vector3.Distance(transform.position + alvos[i], virusperto);
            }
            // cruza
            for (int i = 0; i < population - 2; i += 2)
            {
                Vector3 pai = alvos[torneio(fitness)];
                Vector3 mae = alvos[torneio(fitness)];
                novosAlvos[i] = new Vector3(pai.x, mae.y, -10);
                novosAlvos[i + 1] = new Vector3(mae.x, pai.y, -10);
            }
            // elitiza
            Vector2 melhores = idMelhor(fitness);
            novosAlvos[population - 2] = alvos[(int)melhores.x];
            novosAlvos[population - 1] = alvos[(int)melhores.y];
            // muta
            for(int i = 0; i < population; i++)
            {
                novosAlvos[i].x += Random.Range(-0.1f, 0.1f);
                novosAlvos[i].y += Random.Range(-0.1f, 0.1f);
            }
            alvos = novosAlvos;
        }
        for (int i = 0; i < population; i++) // pra cada alvo
        {
            fitness[i] = Vector3.Distance(transform.position + alvos[i], virusperto);
        }
        return alvos[(int)idMelhor(fitness).x];
    }

    private Vector2 idMelhor(float[] fits)
    {

        Vector2 melhor = new Vector2(fits[0], fits[1]);
        Vector2 idmelhor = new Vector2(0, 1);
        for(int i = 0; i < fits.Length; i++)
        {
            if (fits[i] > melhor.x)
            {
                idmelhor.y = idmelhor.x;
                idmelhor.x = i;
                melhor.y = melhor.x;
                melhor.x = fits[i];
            }
            else if(fits[i] > melhor.y)
            {
                idmelhor.y = i;
                melhor.y = fits[i];
            }
        }
        return idmelhor;
    }

    private int torneio(float[] fits)
    {
        int i, j;
        return fits[i = Random.Range(0, fits.Length)] > fits[j = Random.Range(0, fits.Length)] ? i : j;
    }

    private Vector3 VirusPerto()
    {
        GameObject[] viruss = GameObject.FindGameObjectsWithTag("Virus");
        GameObject[] virusm = GameObject.FindGameObjectsWithTag("VirusDetectado");
        if(viruss.Length < 1)
        {
            return Vector3.zero;
        }
        Vector3 maisPerto = viruss[0].transform.position;
        float menorDist = Vector3.Distance(maisPerto, transform.position);
        for(int i = 1; i < viruss.Length; i++)
        {
            if(Vector3.Distance(transform.position, viruss[i].transform.position) < menorDist) {
                maisPerto = viruss[i].transform.position;
            }
        }
        for(int i = 0; i < virusm.Length; i++)
        {
            if (Vector3.Distance(transform.position, virusm[i].transform.position) < menorDist)
            {
                maisPerto = virusm[i].transform.position;
            }
        }
        return maisPerto;
    }
}