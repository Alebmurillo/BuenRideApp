class Ride < ActiveRecord::Base
  #belongs_to :usuario
  belongs_to :usuario_id,  :foreign_key => 'usuario_id', :class_name => "Usuario"

end
