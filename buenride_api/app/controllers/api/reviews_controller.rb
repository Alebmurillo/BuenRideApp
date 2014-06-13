module Api

class ReviewsController < ApplicationController
   respond_to :json
    PER_PAGE_RECORDS=9
    
    skip_before_filter :verify_authenticity_token

    def index
      reviews= Review.order('id')      
      
      respond_with reviews     
    end
    def new
      
    end
    def create
      @usuario=Usuario.find_by_apikey(params[:apikey])
      if @usuario!=nil
          @review = Review.new({:comentario => params[:comentario], :calificacion=> params[:calificacion]})
          @review.submitted_by=@usuario
          @usuariodest= Usuario.find_by_username(params[:username])
          if @usuariodest!=nil
              @review.usuario = @usuariodest
              @review.save
              respond_with @review, location: nil
          end
          if @usuariodest==nil
              json_response={
                error: 'usuario destino incorrecto'

              }
               respond_with json_response, location: nil
          end
      end
       if @usuario== nil
        json_response={
          error: 'usuario incorrecto'

        }
         respond_with json_response, location: nil
      end
     
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
      params.require(:review).permit(:comentario, :calificacion)
    end
  end
end

