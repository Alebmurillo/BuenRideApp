module Api
class UsuariosController < ApplicationController
  respond_to :json
    PER_PAGE_RECORDS=9
    #before_filter :restrict_access 
    skip_before_filter :verify_authenticity_token
    before_action :authenticate	
    before_action :check_authentication,  except: [ :create, :login ]	
    def logout
      @token = request.headers[:token]
      @user = Usuario.find_by_token(@token) 
      if @user !=nil
        @user.token=nil
        @user.save
        json_response={
            message: 'logout success'
          }
           respond_with json_response, location: nil
      end
       if @user ==nil
        json_response={
              message: 'logout fail'
        }
        respond_with json_response, location: nil
      end
    end
    def login
      
      source = "#{params[:password]}/#{params[:email]}"
      @hashed_password = Digest::SHA2.hexdigest(source)
      @user = Usuario.find_by_email(params[:email]) 
      if @user== nil
        json_response={
          error: 'user or password incorrect'
        }
         respond_with json_response, location: nil
      end
      if @user != nil
          @usuario = Usuario.find_by_email_and_password(params[:email],@hashed_password) 
          if @usuario== nil
            json_response={
              error: 'user or password incorrect'

            }
             respond_with json_response, location: nil
          end
          if @usuario!= nil
    #      json_response={
    #         token: @usuario.token
    #       }
             @token =SecureRandom.hex.to_s
              @usuario.token=@token
              @usuario.save
             respond_with @usuario, location: nil
          end
      end
      
    end  
  
    def getUsuarios
      usuarios= Usuario.order('id')
      
      respond_with usuarios , location: nil
    end
    def new
      
    end
    
    def create
      #self.salt = ActiveSupport::SecureRandom.base64(8)
      source = "#{params[:password]}/#{params[:email]}"
      @hashed_password = Digest::SHA2.hexdigest(source)
      #@hashed_password = BCrypt::Password.create(params[:password])
      #@token =SecureRandom.hex.to_s
      #@persona = Persona.new({:nombre => params[:nombre], :email=> params[:email], :telefono=> params[:telefono],:home_longitud => params[:home_longitud]})
      @usuario = Usuario.new({:username => params[:username], :password => @hashed_password,:nombre => params[:name], :email=> params[:email], :telefono => params[:phone]})
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
#    protected	
#      def authenticate	
#        authenticate_or_request_with_http_token do |token, options|	
#        User.find_by(auth_token: token)	
#    end	
#     end	

    
    
    private
    def usuario_params
      params.require(:usuario).permit(:username , :password, :token,:nombre, :email, :telefono)
      #params.require(:persona).permit(:nombre, :email, :telefono, :home_latitud, :home_longitud)

    end
    
end
end
