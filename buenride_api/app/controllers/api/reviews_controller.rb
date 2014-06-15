module Api

  class ReviewsController < ApplicationController
    respond_to :json
    
    skip_before_filter :verify_authenticity_token
    before_action :authenticate
    
    #obtiene los reviews por el id de usuario
    def getReviews_by_id
      @user = Usuario.find(params[:id])
      @result = Array.new
      @reviews= Review.order('id') 
      @reviews.each do |p|
        if p.usuario_id == @user.id
          @result.push(p)
        end
      end
      respond_with @result   , location: nil  
    end
    #obtiene los reviews por el token
    def myReviews
      @token = request.headers[:token]
      @user = Usuario.find_by_token(@token) 
      @result = Array.new
      @reviews= Review.order('id') 
      @reviews.each do |p|
        if p.usuario_id == @user.id
          @result.push(p)
        end
      end
      respond_with @result   , location: nil   
    end
    #crea el review
    def setReview
      @usuario=Usuario.find_by_token(request.headers[:token])
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
    
    
    private
    def review_params
      params.require(:review).permit(:comentario, :calificacion)
    end
  end
end

