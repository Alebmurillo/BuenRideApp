module Api
  class PersonasController < ApplicationController
    respond_to :json
    PER_PAGE_RECORDS=9
    
    skip_before_filter :verify_authenticity_token
    def index
      personas_paginated= Persona.order('id').page(params[:page]).per(PER_PAGE_RECORDS)
      json_response={
        models: personas_paginated,
        current_page: params[:page].to_i,
        perPage:PER_PAGE_RECORDS,
        total_page: personas_paginated.num_pages
      }
      respond_with json_response     
    end
    def new
      
    end
    def create
      @persona = Persona.new({:nombre => params[:nombre], :email=> params[:email], :telefono=> params[:telefono],:home_longitud => params[:home_longitud]})
      @persona.save
      respond_with @persona, location: nil
    end
    
    def show
      respond_with persona.find(params[:id])
    end
    def update
      respond_with persona.update(params[:id], persona_params)

    end
    def destroy
      respond_with persona.destroy(params[:id])
    end
    private
    def persona_params
      params.require(:persona).permit(:nombre, :email, :telefono, :home_latitud, :home_longitud)
    end
  end
end
