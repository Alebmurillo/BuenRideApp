class Usuario < ActiveRecord::Base
  has_many :reviews, dependent: :destroy
  has_many :rides, dependent: :destroy
  validates :email,:username, :password, :presence =>true
  validates_uniqueness_of :username , :email

  
end
