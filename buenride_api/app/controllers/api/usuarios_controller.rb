module Api
class UsuariosController < ApplicationController
  respond_to :json
    PER_PAGE_RECORDS=9
    #before_filter :restrict_access 
    skip_before_filter :verify_authenticity_token
  
    def login
      @usuario = Usuario.find_by_email_and_password(params[:email],params[:password])
      respond_with @usuario, location: nil
    end  
  
    def index
      usuarios= Usuario.order('id')
      
      respond_with usuarios 
    end
    def new
      
    end
    def create
      @apikey =SecureRandom.hex.to_s
      #@persona = Persona.new({:nombre => params[:nombre], :email=> params[:email], :telefono=> params[:telefono],:home_longitud => params[:home_longitud]})
      @usuario = Usuario.new({:username => params[:username], :password => params[:password], :apikey => @apikey,:nombre => params[:name], :email=> params[:email], :telefono => params[:phone]})
      @usuario.save
      respond_with @usuario, location: nil
    end
#    def createReview
#
#      @usuario = fetch_user()
#      @review = @usuario.reviews.create({:comentario => params[:comentario], :calificacion=> params[:calificacion], :submitted_by=> params[:submitted_by]})
#
#      @usuario.save
#      respond_with @usuario, location: nil
#    end
    def show
      respond_with Usuario.find(params[:id])
    end
    def update
      respond_with Usuario.update(params[:id], usuario_params)

    end
    def destroy
      respond_with Usuario.destroy(params[:id])
    end
    private
    def usuario_params
      params.require(:usuario).permit(:username , :password, :apikey,:nombre, :email, :telefono)
      #params.require(:persona).permit(:nombre, :email, :telefono, :home_latitud, :home_longitud)

    end
end
end
