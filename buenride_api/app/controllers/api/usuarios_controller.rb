module Api
class UsuariosController < ApplicationController
  respond_to :json
    PER_PAGE_RECORDS=9
    
    skip_before_filter :verify_authenticity_token
    def index
      usuarios_paginated= Usuario.order('id').page(params[:page]).per(PER_PAGE_RECORDS)
      json_response={
        models: usuarios_paginated,
        current_page: params[:page].to_i,
        perPage:PER_PAGE_RECORDS,
        total_page: usuarios_paginated.num_pages
      }
      respond_with json_response     
    end
    def new
      
    end
    def create
      @usuario = Usuario.new({:username => params[:username], :password => params[:password], :apikey => params[:apikey]})
      @usuario.save
      respond_with @usuario, location: nil
    end
    
    def show
      respond_with usuario.find(params[:id])
    end
    def update
      respond_with usuario.update(params[:id], usuario_params)

    end
    def destroy
      respond_with usuario.destroy(params[:id])
    end
    private
    def usuario_params
      params.require(:usuario).permit(:username , :password, :apikey)
    end
end
end