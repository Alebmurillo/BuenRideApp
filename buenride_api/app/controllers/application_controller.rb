class ApplicationController < ActionController::Base
  
  APIKEY = 'apikey'
  protect_from_forgery with: :exception
  before_action :authenticate	

  private
  #comprueba que exista el apikey para asi autorizar utilizar el api
  def authenticate
    @apikey = request.headers[:apikey]
    if @apikey==nil || @apikey!= APIKEY
      json_response={
        error: 'autorization error'
      }
      respond_with json_response, location: nil
    end
  end
  #comprueba la autenticacion de usuario, si es un usuario el que accede al api
  def check_authentication
    @token = request.headers[:token]
    @user = Usuario.find_by_token(@token) 
    if @user ==nil
      json_response={
        error: 'authentication error'
      }
      respond_with json_response, location: nil
    end    
    
  end
 
  
end
