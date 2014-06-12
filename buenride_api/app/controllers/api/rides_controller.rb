
module Api
class RidesController < ApplicationController
  respond_to :json
    PER_PAGE_RECORDS=9
    
    skip_before_filter :verify_authenticity_token
  
    def fetch_ride
      @ride = Ride.find_by_id(params[:id])
    end  
  
    def index
      rides= Ride.order('id')
      
      respond_with rides 
    end
    def new
      
    end
    def create
       #:observations
 #:startPointLat
   #  :startPointLong
   # :destPointLat
   #    :destPointLong
   #    
      @usuario=Usuario.find_by_apikey(params[:apikey])
      if @usuario!=nil
      #@persona = Persona.new({:nombre => params[:nombre], :email=> params[:email], :telefono=> params[:telefono],:home_longitud => params[:home_longitud]})
      @ride = Ride.new({:observations => params[:observations], :startPointLat => params[:startPointLat], :startPointLong => params[:startPointLong],:destPointLat => params[:destPointLat], :destPointLong=> params[:destPointLong]})
      
      @ride.usuario= @usuario
      @ride.save

      respond_with @ride, location: nil
      end
       if @usuario== nil
        json_response={
          error: 'usuario incorrecto'

        }
         respond_with json_response, location: nil
      end
      
    end
#    def createReview
#
#      @ride = fetch_ride()
#      @review = @ride.reviews.create({:comentario => params[:comentario], :calificacion=> params[:calificacion], :submitted_by=> params[:submitted_by]})
#
#      @ride.save
#      respond_with @ride, location: nil
#    end
    def show
      respond_with Ride.find(params[:id])
    end
    def update
      respond_with Ride.update(params[:id], ride_params)

    end
    def destroy
      respond_with Ride.destroy(params[:id])
    end
    
   
    private
    def ride_params
      params.require(:ride).permit(:observations , :startPointLat, :startPointLong,:destPointLat, :destPointLong)
      #params.require(:persona).permit(:nombre, :email, :telefono, :home_latitud, :home_longitud)

    end
end
end
