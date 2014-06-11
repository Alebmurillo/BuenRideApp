module Api
class LugarsController < ApplicationController
  respond_to :json
    PER_PAGE_RECORDS=9
    
    skip_before_filter :verify_authenticity_token
    def index
      lugars_paginated= Lugar.order('id').page(params[:page]).per(PER_PAGE_RECORDS)
      json_response={
        models: lugars_paginated,
        current_page: params[:page].to_i,
        perPage:PER_PAGE_RECORDS,
        total_page: lugars_paginated.num_pages
      }
      respond_with json_response     
    end
    def new
      
    end
    def create
      @lugar = Lugar.new({:nombre_lugar => params[:nombre_lugar], :lugar_longitud => params[:lugar_longitud], :lugar_latitud => params[:lugar_latitud]})
      @lugar.save
      respond_with @lugar, location: nil
    end
    
    def show
      respond_with lugar.find(params[:id])
    end
    def update
      respond_with lugar.update(params[:id], lugar_params)

    end
    def destroy
      respond_with lugar.destroy(params[:id])
    end
    private
    def lugar_params
      params.require(:lugar).permit(:nombre_lugar , :lugar_latitud, :lugar_longitud)
    end
end
end
