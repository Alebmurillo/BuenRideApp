class Usuario < ActiveRecord::Base
    has_many :reviews, dependent: :destroy
    #validates :email, :first_name, :last_name, :presence =>true
    validates_uniqueness_of :username
end
