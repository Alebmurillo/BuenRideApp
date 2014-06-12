class Review < ActiveRecord::Base
  belongs_to :usuario
  belongs_to :submitted_by,  :foreign_key => 'submitted_by', :class_name => "Usuario"
  
end
