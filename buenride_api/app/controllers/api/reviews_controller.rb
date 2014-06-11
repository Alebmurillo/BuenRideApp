module Api

class ReviewsController < ApplicationController
   respond_to :json
    PER_PAGE_RECORDS=9
    
    skip_before_filter :verify_authenticity_token
    def index
      reviews_paginated= Review.order('id').page(params[:page]).per(PER_PAGE_RECORDS)
      json_response={
        models: reviews_paginated,
        current_page: params[:page].to_i,
        perPage:PER_PAGE_RECORDS,
        total_page: reviews_paginated.num_pages
      }
      respond_with json_response     
    end
    def new
      
    end
    def create
      @review = Review.new({:comentario => params[:comentario], :calificacion=> params[:calificacion], :submitted_by=> params[:submitted_by]})
      @review.save
      respond_with @review, location: nil
    end
    
    def show
      respond_with review.find(params[:id])
    end
    def update
      respond_with review.update(params[:id], review_params)

    end
    def destroy
      respond_with review.destroy(params[:id])
    end
    private
    def review_params
      params.require(:review).permit(:comentario, :calificacion, :submitter_by)
    end
  end
end

