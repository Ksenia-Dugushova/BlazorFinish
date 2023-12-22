using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using BlazorApp1.Class;

namespace BlazorApp1.Class
{
    public class Movie
    {

        public string movieTitles { get; set; }


        //public List<Person> People { get; set; }
        public List<Person> director { get; set; }

        //movie.actor.add(person)
        public List<Person> actors { get; set; }
        //[NotMapped]
        public HashSet<Tag> tag = new HashSet<Tag>();

        public string movieRating = "0.0";
        [Key]
        public string MovieCode { get; set; }

        // Структура для хранения информации о похожем фильме
        public struct SimilarMovieInfo
        {
            public string MovieTitle { get; set; }
            public int SimilarityScore { get; set; }
        }


        // Словарь для хранения информации о топ 10 похожих фильмах
        [NotMapped]
        public Dictionary<string, List<SimilarMovieInfo>> TopSimilarMovies { get; private set; } = new Dictionary<string, List<SimilarMovieInfo>>();


        //public string idTag = "";

        public Movie(string title, string code)
        {
            movieTitles = title;
            MovieCode = code;
            director = new List<Person>();
            actors = new List<Person>();
        }

        public Movie()
        {

        }



        public int CompareTo(Movie movie)
        {
            int answer = Convert.ToInt32(movie.movieRating.Replace(".", ""));
            if (actors != null && movie.actors != null)
            {
                int k = 0;
                foreach (Person actor in actors)
                {
                    if (movie.actors.Contains(actor))
                    {
                        k++;
                    }
                }
                answer += 10 * k;
                //answer += (k / this.Actors.Count * 10) * 1 / 8;
            }
            if (director != null && movie.director != null)
            {
                int k = 0;
                foreach (Person director in director)
                {
                    if (movie.director.Contains(director))
                    {
                        k++;
                    }
                }
                answer += 20 * k;
                //answer += (k / this.Director.Count * 10) * 1 / 8;
            }
            if (tag != null && movie.tag != null)
            {
                int k = 0;
                foreach (Tag tag in tag)
                {
                    if (movie.tag.Contains(tag))
                    {
                        k++;
                    }
                }
                answer += k;
                //answer += (k / this.Tags.Count * 10) * 1 / 4;
            }
            return answer;
        }
        public LinkedList<Movie> list_top10 = new LinkedList<Movie>();
        public LinkedList<int> numb_of_top10 = new LinkedList<int>();
        public string codes_of_top10 = "";
        public void FindSimilarMovies()
        {
            LinkedListNode<Movie> tMovie;
            LinkedListNode<int> tInt;
            HashSet<Movie> closeMovies = new HashSet<Movie>();


            if (director != null)
            {
                foreach (Person dir in director)
                {
                    if (dir.Movies != null)
                    {
                        foreach (Movie movie in dir.Movies)
                        {
                            closeMovies.Add(movie);
                        }
                    }
                }
            }

            if (actors != null)
            {
                foreach (Person actor in actors)
                {
                    if (actor != null)
                    {
                        if (actor.Movies != null)
                        {
                            foreach (Movie movie in actor.Movies)
                            {
                                closeMovies.Add(movie);
                            }
                        }
                    }
                }
            }

            if (tag != null)//(Tags != null && Top10.Count < 10)
            {
                foreach (Tag tag in tag)
                {
                    if (tag.Movies != null)
                    {
                        foreach (Movie movie in tag.Movies)
                        {
                            closeMovies.Add(movie);
                        }
                    }
                }
            }
            foreach (Movie movie in closeMovies)
            {
                if (movie != this)
                {
                    int r = CompareTo(movie);
                    if (list_top10.Count == 0)
                    {
                        list_top10.AddFirst(movie);
                        numb_of_top10.AddFirst(r);
                    }
                    else
                    {
                        tMovie = list_top10.First;
                        tInt = numb_of_top10.First;
                        while (tInt.Next != null && tInt.Value > r)
                        {
                            tMovie = tMovie.Next;
                            tInt = tInt.Next;
                        }
                        if (tInt.Value < r)
                        {
                            list_top10.AddBefore(tMovie, movie);
                            numb_of_top10.AddBefore(tInt, r);
                        }
                        else
                        {
                            list_top10.AddLast(movie);
                            numb_of_top10.AddLast(r);
                        }
                        while (list_top10.Count > 10)
                        {
                            list_top10.RemoveLast();
                            numb_of_top10.RemoveLast();
                        }
                    }
                }
            }
            foreach (Movie movie in list_top10)
            {
                if (codes_of_top10 == "")
                {
                    codes_of_top10 = movie.MovieCode;
                }
                else
                {
                    codes_of_top10 += "|" + movie.MovieCode;
                }
            }
        }

    }


}
