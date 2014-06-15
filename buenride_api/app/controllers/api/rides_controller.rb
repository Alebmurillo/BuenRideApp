
module Api
class RidesController < ApplicationController
  respond_to :json
    PER_PAGE_RECORDS=9
    
    skip_before_filter :verify_authenticity_token
        before_action :authenticate	
        
    def find_by_route
      @result = Array.new
      @distancias = Array.new

      @ride = Ride.order('id')
      if params[:radio] != '0'
        @ride.each do |p|
          distancia1 =Geocoder::Calculations.distance_between([params[:destLatitud],params[:destLongitud]], [p.destPointLat,p.destPointLong])
          distancia2 =Geocoder::Calculations.distance_between([params[:startLatitud],params[:startLongitud]], [p.startPointLat,p.startPointLong])
          if distancia1 <params[:radio].to_f &&  distancia2 <params[:radio].to_f
            @result.push(p)
            #@distancias.push(distancia1)
          end
        end
#        json_response={
#            result: @result,
#            distancias: @distancias
#          }         
        respond_with @result , location: nil
      end
      if params[:radio] == '0'
        respond_with @ride,location: nil
      end      
        #Ride.find_by_destPointLat_and_destPointLong(params[:latitud],params[:longitud])
      
    end
    
#    def find_by_start
#      @result = Array.new
#      @distancias = Array.new
#
#      @ride = Ride.order('id')
#      if params[:radio] != '0'
#          @ride.each do |p|
#            distancia =Geocoder::Calculations.distance_between([params[:latitud],params[:longitud]], [p.startPointLat,p.startPointLong])
#            if distancia <params[:radio].to_f 
#              @result.push(p)
#              @distancias.push(distancia)
#            end
#          end
##          json_response={
##              result: @result,
##              distancias: @distancias
##            }         
#          respond_with @result,location: nil      
#            #Ride.find_by_destPointLat_and_destPointLong(params[:latitud],params[:longitud])
#       end
#      if params[:radio] == '0'
#        respond_with @ride,location: nil
#      end
#    end
    
    def find_by_user
      @result = Array.new
      @usuario = Usuario.where('username LIKE ?', "%#{params[:search]}%")
      @usuario.each do |p|
        @ride= Ride.order('id')
        @result = Array.new
        
        @ride.each do |q|
          if q.usuario_id == p.id
            @result.push(p)
          end
        end
        
      end
      
    
#      json_response={
#          result: @result
#        }
     #@result.push(@ride.usuario_id.attributes.except('password','token','created_at','updated_at'))

      #@ride = Ride.where('observations LIKE ?', "%#{params[:search]}%")
      
      #@usuario = Usuario.where('username LIKE ?', "%#{params[:search]}%")
      respond_with @result,location: nil
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
      
      @ride.usuario= @usuario
      #@ride.usuario_id = @usuario
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
