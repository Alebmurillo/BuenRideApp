
module Api
  class RidesController < ApplicationController
    respond_to :json
    
    skip_before_filter :verify_authenticity_token
    before_action :authenticate	
        
  #busca rides por la distancia a la que se encuentran
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
    
 
    def find_by_user
      @usuarios = Usuario.where('username LIKE ?', "%#{params[:search]}%")
      @result = Array.new 
      @usuarios.each do |p|
        @result.push(p.attributes.except('password','token','created_at','updated_at'))
      end
   
      respond_with @result,location: nil
    end  
  
    #crea un ride
    def addRide
     
      @usuario=Usuario.find_by_token(request.headers[:token])
      if @usuario!=nil
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
      
    private
    def ride_params
      params.require(:ride).permit(:observations , :startPointLat, :startPointLong,:destPointLat, :destPointLong)

    end
  end
end
