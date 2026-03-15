using System;

namespace SidiReviews.Helpers
{

    public class ReviewFormNavigationParameter
    {
        public int? MovieId { get; init; }  
        public int? ReviewId { get; init; } 

        public ReviewFormNavigationParameter(int? movieId = null, int? reviewId = null)
        {
            if (movieId.HasValue && reviewId.HasValue)
                throw new ArgumentException("Não pode fornecer MovieId e ReviewId ao mesmo tempo.");
            if (!movieId.HasValue && !reviewId.HasValue)
                throw new ArgumentException("Precisa fornecer MovieId (para adicionar) ou ReviewId (para editar).");

            MovieId = movieId;
            ReviewId = reviewId;
        }
    }
}
