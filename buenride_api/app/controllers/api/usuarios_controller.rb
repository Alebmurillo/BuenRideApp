module Api
  class UsuariosController < ApplicationController
    respond_to :json
    
    skip_before_filter :verify_authenticity_token
    before_action :authenticate	
    before_action :check_authentication,  except: [ :registrar, :login ]	
    #elimina el token de la BD
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
    #crea un toquen para el usuario y asi pueda utilizar el api
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
          
          @token =SecureRandom.hex.to_s
             
          @usuario.token=@token
          @usuario.save
          respond_with @usuario.attributes.except('password','created_at','updated_at'), location: nil
        end
      end
      
    end
    #obtiene el usuario por id
    def getUsuario_by_id
      @usuarios= Usuario.find(params[:id])      
      respond_with @usuarios.attributes.except('password','token','created_at','updated_at'), location: nil
    end
    #obtiene el usuario por token
    def myUsuario
      @token = request.headers[:token]
      @user = Usuario.find_by_token(@token) 
      respond_with @user, location: nil
    end
  #obtiene todos los usuarios
    def getUsuarios
      @usuarios= Usuario.order('id')
      @result = Array.new 
      @usuarios.each do |p|
        @result.push(p.attributes.except('password','token','created_at','updated_at'))
      end
      respond_with @result, location: nil
    end
    
    #crea un usuario
    def registrar
      source = "#{params[:password]}/#{params[:email]}"
      @hashed_password = Digest::SHA2.hexdigest(source)
      @usuario = Usuario.new({:username => params[:username], :password => @hashed_password,:nombre => params[:name], :email=> params[:email], :telefono => params[:phone]})
      @usuario.save
      respond_with @usuario, location: nil
    end
   
    
    
    private
    def usuario_params
      params.require(:usuario).permit(:username , :password, :token,:nombre, :email, :telefono)

    end
    
  end
end
