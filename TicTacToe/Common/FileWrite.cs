using System;
using System.IO;
using System.Text.Json;

namespace TicTacToe.Common
{
    static class FileWrite
    {
        const string pathToScores = "scores.txt";
        const string pathToTotalScores = "scores_total.json";
        static FileWrite()
        {

        }
        static public void WriteWinner(Owner player)
        {
            using (StreamWriter streamWriter = new StreamWriter(pathToScores, true))
            {
                if (player == Owner.None)
                {
                    streamWriter.WriteLine($"Draw at {DateTime.Now}");
                }
                else
                {
                    streamWriter.WriteLine($"Winner: {player} at {DateTime.Now}");
                }
            }
        }
        static public void WriteTotalScore(Scores score)
        {
            using (StreamWriter streamWriter = new StreamWriter(pathToTotalScores))
            {

                string s = JsonSerializer.Serialize<Scores>(score);
                streamWriter.WriteLine(s);
            }
        }
        static public Scores ReadScores()
        {
            try
            {
                string s = File.ReadAllText(pathToTotalScores);
                Scores score = JsonSerializer.Deserialize<Scores>(s);
                return score;

            }
            catch (FileNotFoundException)
            {
                return new Scores();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
    }
}
