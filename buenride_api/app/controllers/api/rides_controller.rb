
module Api
class RidesController < ApplicationController
  respond_to :json
    PER_PAGE_RECORDS=9
    
    skip_before_filter :verify_authenticity_token
        before_action :authenticate	
        
    def find_by_destiny
      @result = Array.new
      @distancias = Array.new

      @ride = Ride.order('id')
      if params[:radio] != nil
        @ride.each do |p|
          distancia =Geocoder::Calculations.distance_between([params[:latitud],params[:longitud]], [p.destPointLat,p.destPointLong])
          if distancia <params[:radio].to_f 
            @result.push(p)
            @distancias.push(distancia)
          end
        end
        json_response={
            result: @result,
            distancias: @distancias
          }         
        respond_with json_response,location: nil
      end
      if params[:radio] == nil
        respond_with @ride,location: nil
      end
      
        #Ride.find_by_destPointLat_and_destPointLong(params[:latitud],params[:longitud])
      
    end
    
    def find_by_start
      @result = Array.new
      @distancias = Array.new

      @ride = Ride.order('id')
      if params[:radio] != nil
          @ride.each do |p|
            distancia =Geocoder::Calculations.distance_between([params[:latitud],params[:longitud]], [p.startPointLat,p.startPointLong])
            if distancia <params[:radio].to_f 
              @result.push(p)
              @distancias.push(distancia)
            end
          end
          json_response={
              result: @result,
              distancias: @distancias
            }         
          respond_with json_response,location: nil      
            #Ride.find_by_destPointLat_and_destPointLong(params[:latitud],params[:longitud])
       end
      if params[:radio] == nil
        respond_with @ride,location: nil
      end
    end
    
    def find_by_user
      @result = Array.new
      @usuario = Usuario.where('username LIKE ?', "%#{params[:search]}%")
      @usuario.each do |p|
        @ride = Ride.find(p.id)
        if @ride != nil
          @result.push(@ride)
        end
        
      end
      json_response={
          result: @result
        }
      
      #@ride = Ride.where('observations LIKE ?', "%#{params[:search]}%")
      
      #@usuario = Usuario.where('username LIKE ?', "%#{params[:search]}%")
      respond_with json_response,location: nil
    end  
  
    def index
      #@hola =  Geocoder.coordinates "Ukraine"
      #respond_with @hola
      rides = Ride.order('id')
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
      @usuario=Usuario.find_by_token(request.headers[:token])
      if @usuario!=nil
      #@persona = Persona.new({:nombre => params[:nombre], :email=> params[:email], :telefono=> params[:telefono],:home_longitud => params[:home_longitud]})
      @ride = Ride.new({:observations => params[:observations], :startPointLat => params[:startPointLat], :startPointLong => params[:startPointLong],:destPointLat => params[:destPointLat], :destPointLong=> params[:destPointLong]})
      
      #@ride.usuario= @usuario
      @ride.usuario_id = @usuario
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
