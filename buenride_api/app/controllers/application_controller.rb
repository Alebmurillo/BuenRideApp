class ApplicationController < ActionController::Base
  # Prevent CSRF attacks by raising an exception.
  # For APIs, you may want to use :null_session instead.
  APIKEY = 'apikey'
  protect_from_forgery with: :exception
      before_action :authenticate	

   private
   def authenticate
     @apikey = request.headers[:apikey]
     if @apikey==nil || @apikey!= APIKEY
       json_response={
          error: 'autorization error'
        }
         respond_with json_response, location: nil
     end
   end
     def check_authentication
     @token = request.headers[:token]
     @user = Usuario.find_by_apikey(@token) 
      if @user ==nil
       json_response={
          error: 'authentication error'
        }
         respond_with json_response, location: nil
     end    
    
   end
#
#    def restrict_access
#      unless restrict_access_by_params || restrict_access_by_header
#        render json: {message: 'Invalid API Token'}, status: 401
#        return
#      end
#
#      @current_user = @api_key.user if @api_key
#    end
#
#    def restrict_access_by_header
#      return true if @api_key
#
#      authenticate_with_http_token do |token|
#        @api_key = Usuario.find_by_apikey(token)
#      end
#    end
#
#    def restrict_access_by_params
#      return true if @api_key
#
#      @api_key = Usuario.find_by_apikey(params[:token])
#    end
  
  
  
end
