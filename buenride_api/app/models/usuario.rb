class Usuario < ActiveRecord::Base
  has_many :reviews, dependent: :destroy
  has_many :rides, dependent: :destroy
  #has_many :reviews, :foreign_key => 'usuario_id', :class_name => "Usuario", dependent: :destroy
  validates :email,:username, :password, :presence =>true
  validates_uniqueness_of :username , :email

  
end
