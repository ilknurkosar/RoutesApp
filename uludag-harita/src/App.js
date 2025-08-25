import React, { useState, useEffect } from 'react';
import { 
  Search, Menu, X, MapPin, Clock, Flower2, Navigation, User, Settings, 
  Star, History, Bookmark, Mountain, Car, Bike, Footprints, Zap, AlertCircle, 
  RefreshCw, Eye, EyeOff, Mail, Lock, Phone, Calendar, Users, Camera,
  Share2, Download, Heart, MessageCircle, Shield, Award, Compass,
  Thermometer, Wind, CloudRain, Sun, Moon, Wifi, Battery, Bell,
  ChevronRight, Play, Pause, RotateCcw, Filter, SortDesc
} from 'lucide-react';

// Mock API Service
const ApiService = {
  async getRotalar() {
    return [
      {
        id: 1,
        adi: "Sarıalan Yürüyüş Rotası",
        mesafe: 3.2,
        tahminiSureDakika: 45,
        zorluk: "Kolay",
        renk: "#22c55e",
        tip: "walking",
        aciklama: "Ailenizle keyifli bir doğa yürüyüşü yapabileceğiniz kolay rota",
        yukseklik: 1750,
        resimler: [
          "https://images.unsplash.com/photo-1464822759844-d150baec843f?w=800",
          "https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=800"
        ],
        ozellikler: ["Aile Dostu", "Fotoğraf Noktaları", "Piknik Alanı"]
      },
      {
        id: 2,
        adi: "Zirve Tırmanış Rotası",
        mesafe: 8.5,
        tahminiSureDakika: 180,
        zorluk: "Zor",
        renk: "#ef4444",
        tip: "walking",
        aciklama: "Deneyimli dağcılar için zorlu zirve tırmanışı",
        yukseklik: 2543,
        resimler: [
          "https://images.unsplash.com/photo-1551632811-561732d1e306?w=800"
        ],
        ozellikler: ["Deneyim Gerekli", "Zirve Manzarası", "Zorlu Parkur"]
      },
      {
        id: 3,
        adi: "Bisiklet Safari Rotası",
        mesafe: 12.3,
        tahminiSureDakika: 90,
        zorluk: "Orta",
        renk: "#3b82f6",
        tip: "cycling",
        aciklama: "Doğal güzellikler arasında bisiklet turu",
        yukseklik: 1900,
        resimler: [
          "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?w=800"
        ],
        ozellikler: ["Doğa Manzarası", "Orta Zorluk", "Bisiklet Dostu"]
      },
      {
        id: 4,
        adi: "ATV Macera Rotası",
        mesafe: 15.7,
        tahminiSureDakika: 120,
        zorluk: "Orta",
        renk: "#f59e0b",
        tip: "atv",
        aciklama: "ATV ile adrenalin dolu macera rotası",
        yukseklik: 2100,
        resimler: [
          "https://images.unsplash.com/photo-1544197150-b99a580bb7a8?w=800"
        ],
        ozellikler: ["Adrenalin", "Off-Road", "Rehber Eşliği"]
      },
      {
        id: 5,
        adi: "Araç Touring Rotası",
        mesafe: 25.4,
        tahminiSureDakika: 60,
        zorluk: "Kolay",
        renk: "#8b5cf6",
        tip: "driving",
        aciklama: "Araçla keyifli bir tur rotası",
        yukseklik: 1800,
        resimler: [
          "https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=800"
        ],
        ozellikler: ["Araç Uygun", "Manzara", "Kolay Erişim"]
      }
    ];
  }
};

const ULUDAG_CENTER = [40.0917, 29.0750];

const UludagParkMap = () => {
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [selectedRoute, setSelectedRoute] = useState(null);
  const [activeLayer, setActiveLayer] = useState('all');
  const [searchQuery, setSearchQuery] = useState('');
  const [showLogin, setShowLogin] = useState(false);
  const [showRegister, setShowRegister] = useState(false);
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [user, setUser] = useState(null);
  const [currentImageIndex, setCurrentImageIndex] = useState(0);
  
  // API state'leri
  const [rotalar, setRotalar] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [favorites, setFavorites] = useState([]);
  const [weather] = useState({
    temp: 18,
    condition: 'sunny',
    humidity: 65,
    wind: 12
  });

  // Rota tipini belirleme
  const getRouteTypeInfo = (tip) => {
    const types = {
      walking: { name: 'Yürüyüş', icon: Footprints, color: '#22c55e' },
      cycling: { name: 'Bisiklet', icon: Bike, color: '#3b82f6' },
      driving: { name: 'Araç', icon: Car, color: '#8b5cf6' },
      atv: { name: 'ATV', icon: Zap, color: '#f59e0b' }
    };
    return types[tip] || types.walking;
  };

  // API'den rotaları çek
  useEffect(() => {
    const fetchRotalar = async () => {
      try {
        setLoading(true);
        setError(null);
        const data = await ApiService.getRotalar();
        setRotalar(data || []);
      } catch (err) {
        console.error('Rotalar yüklenirken hata:', err);
        setError(`Rotalar yüklenirken hata oluştu: ${err.message}`);
      } finally {
        setLoading(false);
      }
    };
    fetchRotalar();
  }, []);

  // Login fonksiyonu
  const handleLogin = (email, password) => {
    setUser({ name: 'Mehmet Yılmaz', email, avatar: null });
    setIsLoggedIn(true);
    setShowLogin(false);
  };

  // Register fonksiyonu
  const handleRegister = (userData) => {
    setUser({ name: userData.name, email: userData.email, avatar: null });
    setIsLoggedIn(true);
    setShowRegister(false);
  };

  // Favorilere ekleme/çıkarma
  const toggleFavorite = (routeId) => {
    setFavorites(prev => 
      prev.includes(routeId) 
        ? prev.filter(id => id !== routeId)
        : [...prev, routeId]
    );
  };

  // Rota seçme
  const selectRoute = (route) => {
    setSelectedRoute(route);
    setCurrentImageIndex(0);
  };

  // Rotaları kategorilere ayırma
  const groupedRoutes = rotalar.reduce((groups, rota) => {
    const typeInfo = getRouteTypeInfo(rota.tip);
    if (!groups[rota.tip]) {
      groups[rota.tip] = {
        name: typeInfo.name,
        color: typeInfo.color,
        icon: typeInfo.icon,
        paths: []
      };
    }
    
    groups[rota.tip].paths.push(rota);
    return groups;
  }, {});

  // Loading durumu
  if (loading) {
    return (
      <div className="h-screen w-full flex items-center justify-center bg-gradient-to-br from-emerald-100 via-blue-50 to-green-100">
        <div className="text-center">
          <Mountain className="w-16 h-16 text-green-500 mx-auto animate-bounce mb-4" />
          <h2 className="text-2xl font-bold text-gray-800 mb-2">Uludağ Milli Parkı</h2>
          <div className="animate-spin rounded-full h-12 w-12 border-4 border-green-500 border-t-transparent mx-auto mb-4"></div>
          <p className="text-gray-600 font-medium">Harita yükleniyor...</p>
        </div>
      </div>
    );
  }

  return (
    <div className="h-screen w-full bg-gray-50 relative overflow-hidden">
      {/* Ana Harita Mockup */}
      <div className="absolute inset-0 bg-gradient-to-br from-green-100 via-blue-50 to-emerald-100">
        <div className="absolute inset-0 opacity-30">
          <svg className="w-full h-full" viewBox="0 0 800 600">
            {rotalar.map((rota, index) => {
              const typeInfo = getRouteTypeInfo(rota.tip);
              if (activeLayer !== 'all' && activeLayer !== rota.tip) return null;
              
              return (
                <g key={rota.id}>
                  <path
                    d={`M ${100 + index * 150} 200 Q ${200 + index * 100} ${150 + index * 50} ${300 + index * 120} ${250 + index * 40}`}
                    stroke={typeInfo.color}
                    strokeWidth="4"
                    fill="none"
                    className="cursor-pointer hover:stroke-width-6 transition-all"
                    onClick={() => selectRoute(rota)}
                  />
                  <circle
                    cx={100 + index * 150}
                    cy={200}
                    r="6"
                    fill={typeInfo.color}
                    className="cursor-pointer hover:r-8 transition-all"
                    onClick={() => selectRoute(rota)}
                  />
                </g>
              );
            })}
          </svg>
        </div>

        {/* Google Maps Tarzı Kategori Butonları */}
        <div className="absolute top-4 left-1/2 transform -translate-x-1/2 z-[1000]">
          <div className="flex gap-2 bg-white/95 backdrop-blur-sm rounded-full p-2 shadow-lg">
            <button
              onClick={() => setActiveLayer('all')}
              className={`flex items-center gap-2 px-4 py-2 rounded-full transition-all duration-200 font-medium text-sm ${
                activeLayer === 'all' 
                  ? 'bg-green-500 text-white shadow-md' 
                  : 'text-gray-700 hover:bg-gray-100'
              }`}
            >
              <MapPin className="w-4 h-4" />
              <span>Tümü</span>
              <span className="bg-white/20 text-xs px-1.5 py-0.5 rounded-full">
                {rotalar.length}
              </span>
            </button>
            
            {Object.entries(groupedRoutes).map(([key, route]) => {
              const IconComponent = route.icon;
              return (
                <button
                  key={key}
                  onClick={() => setActiveLayer(activeLayer === key ? 'all' : key)}
                  className={`flex items-center gap-2 px-4 py-2 rounded-full transition-all duration-200 font-medium text-sm ${
                    activeLayer === key 
                      ? 'text-white shadow-md' 
                      : 'text-gray-700 hover:bg-gray-100'
                  }`}
                  style={activeLayer === key ? { backgroundColor: route.color } : {}}
                >
                  <IconComponent className="w-4 h-4" />
                  <span>{route.name}</span>
                  <span className={`text-xs px-1.5 py-0.5 rounded-full ${
                    activeLayer === key ? 'bg-white/20' : 'bg-gray-100'
                  }`}>
                    {route.paths.length}
                  </span>
                </button>
              );
            })}
          </div>
        </div>

        {/* Hava Durumu Widget */}
        <div className="absolute top-4 right-4 z-[1000] bg-white/95 backdrop-blur-sm rounded-xl p-3 shadow-lg">
          <div className="flex items-center gap-2">
            <Sun className="w-5 h-5 text-yellow-500" />
            <div>
              <div className="font-bold text-gray-800 text-lg">{weather.temp}°</div>
              <div className="text-xs text-gray-600">Güneşli</div>
            </div>
          </div>
        </div>
      </div>

      {/* Login/Register Popup */}
      {(showLogin || showRegister) && (
        <div className="absolute inset-0 bg-black/50 flex items-center justify-center z-[2000]">
          <div className="bg-white rounded-2xl p-8 max-w-md w-full mx-4 shadow-2xl">
            <div className="flex justify-between items-center mb-6">
              <h2 className="text-2xl font-bold text-gray-800">
                {showLogin ? 'Giriş Yap' : 'Kayıt Ol'}
              </h2>
              <button 
                onClick={() => {setShowLogin(false); setShowRegister(false);}}
                className="p-2 hover:bg-gray-100 rounded-lg"
              >
                <X className="w-5 h-5" />
              </button>
            </div>

            {showLogin ? (
              <LoginForm onLogin={handleLogin} onSwitchToRegister={() => {setShowLogin(false); setShowRegister(true);}} />
            ) : (
              <RegisterForm onRegister={handleRegister} onSwitchToLogin={() => {setShowRegister(false); setShowLogin(true);}} />
            )}
          </div>
        </div>
      )}

      {/* Sol Sidebar */}
      <div className={`absolute left-0 top-0 h-full bg-white shadow-2xl z-[1000] transition-all duration-300 ease-in-out ${
        sidebarOpen ? 'w-96' : 'w-0'
      } overflow-hidden`}>
        <div className="p-6 h-full overflow-y-auto">
          <div className="flex items-center justify-between mb-6">
            <div>
              <h2 className="text-2xl font-bold text-gray-800">Rotalar</h2>
              <p className="text-sm text-gray-600">{rotalar.length} rota keşfet</p>
            </div>
            <button 
              onClick={() => setSidebarOpen(false)}
              className="p-2 hover:bg-gray-100 rounded-lg transition-colors"
            >
              <X className="w-5 h-5" />
            </button>
          </div>

          {/* Arama */}
          <div className="relative mb-6">
            <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 w-4 h-4 text-gray-400" />
            <input
              type="text"
              placeholder="Rota ara..."
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
              className="w-full pl-10 pr-4 py-3 border border-gray-200 rounded-xl focus:ring-2 focus:ring-green-500 focus:border-transparent outline-none"
            />
          </div>

          {/* Rota Listesi */}
          <div className="space-y-4">
            {rotalar
              .filter(rota => 
                (activeLayer === 'all' || activeLayer === rota.tip) &&
                rota.adi.toLowerCase().includes(searchQuery.toLowerCase())
              )
              .map((rota) => {
                const typeInfo = getRouteTypeInfo(rota.tip);
                const IconComponent = typeInfo.icon;
                
                return (
                  <div 
                    key={rota.id} 
                    className={`bg-white rounded-xl p-4 shadow-sm hover:shadow-lg transition-all cursor-pointer border-2 ${
                      selectedRoute?.id === rota.id ? 'border-green-500' : 'border-gray-100 hover:border-gray-200'
                    }`}
                    onClick={() => selectRoute(rota)}
                  >
                    <div className="flex items-start gap-3">
                      <div className="p-2 rounded-lg" style={{ backgroundColor: `${typeInfo.color}20` }}>
                        <IconComponent className="w-5 h-5" style={{ color: typeInfo.color }} />
                      </div>
                      
                      <div className="flex-1">
                        <div className="flex justify-between items-start mb-2">
                          <h4 className="font-semibold text-gray-800">{rota.adi}</h4>
                          <button
                            onClick={(e) => {
                              e.stopPropagation();
                              toggleFavorite(rota.id);
                            }}
                            className="p-1 hover:bg-gray-100 rounded-full transition-colors"
                          >
                            <Heart 
                              className={`w-4 h-4 ${
                                favorites.includes(rota.id) ? 'fill-red-500 text-red-500' : 'text-gray-400'
                              }`} 
                            />
                          </button>
                        </div>
                        
                        <div className="space-y-1 text-sm text-gray-600">
                          <div className="flex justify-between">
                            <span>Mesafe:</span>
                            <span className="font-medium">{rota.mesafe} km</span>
                          </div>
                          <div className="flex justify-between">
                            <span>Süre:</span>
                            <span className="font-medium">
                              {rota.tahminiSureDakika >= 60 ? 
                                `${Math.floor(rota.tahminiSureDakika / 60)}s ${rota.tahminiSureDakika % 60}dk` : 
                                `${rota.tahminiSureDakika}dk`}
                            </span>
                          </div>
                          <div className="flex justify-between">
                            <span>Zorluk:</span>
                            <span className={`font-medium px-2 py-0.5 rounded-full text-xs ${
                              rota.zorluk === 'Kolay' ? 'bg-green-100 text-green-700' :
                              rota.zorluk === 'Orta' ? 'bg-yellow-100 text-yellow-700' :
                              'bg-red-100 text-red-700'
                            }`}>
                              {rota.zorluk}
                            </span>
                          </div>
                        </div>

                        {/* Özellikler */}
                        <div className="flex flex-wrap gap-1 mt-3">
                          {rota.ozellikler?.slice(0, 3).map((ozellik, index) => (
                            <span key={index} className="text-xs bg-blue-100 text-blue-700 px-2 py-1 rounded-full">
                              {ozellik}
                            </span>
                          ))}
                        </div>
                      </div>
                    </div>
                  </div>
                );
              })}
          </div>
        </div>
      </div>

      {/* Sağ Rota Detay Paneli */}
      {selectedRoute && (
        <RouteDetailPanel 
          route={selectedRoute} 
          onClose={() => setSelectedRoute(null)}
          currentImageIndex={currentImageIndex}
          setCurrentImageIndex={setCurrentImageIndex}
          favorites={favorites}
          toggleFavorite={toggleFavorite}
        />
      )}

      {/* Menu Toggle Button */}
      <button
        onClick={() => setSidebarOpen(!sidebarOpen)}
        className="absolute top-4 left-4 z-[1001] bg-white shadow-lg rounded-xl p-3 hover:shadow-xl transition-all duration-200 hover:scale-105"
      >
        <Menu className="w-6 h-6 text-gray-700" />
      </button>

      {/* Kullanıcı Menüsü */}
      <div className="absolute top-4 right-20 z-[1001]">
        {isLoggedIn ? (
          <UserMenu user={user} onLogout={() => {setIsLoggedIn(false); setUser(null);}} />
        ) : (
          <div className="flex gap-2">
            <button
              onClick={() => setShowLogin(true)}
              className="bg-white/95 backdrop-blur-sm shadow-lg rounded-xl px-4 py-2 hover:shadow-xl transition-all duration-200 hover:scale-105 font-medium text-gray-700 border border-gray-200"
            >
              Giriş
            </button>
            <button
              onClick={() => setShowRegister(true)}
              className="bg-green-500 shadow-lg rounded-xl px-4 py-2 hover:shadow-xl transition-all duration-200 hover:scale-105 font-medium text-white hover:bg-green-600"
            >
              Kayıt Ol
            </button>
          </div>
        )}
      </div>

      {/* Alt Bilgi Paneli */}
      <div className="absolute bottom-6 left-1/2 transform -translate-x-1/2 z-[1000]">
        <div className="bg-white/95 backdrop-blur-sm rounded-2xl shadow-lg p-4 min-w-[400px]">
          <div className="text-center mb-3">
            <div className="flex items-center justify-center gap-2 mb-2">
              <Mountain className="w-6 h-6 text-green-600" />
              <h4 className="font-bold text-gray-800 text-lg">Uludağ Milli Parkı</h4>
            </div>
            <p className="text-sm text-gray-600">{rotalar.length} aktif rota • Canlı harita sistemi</p>
            <div className="text-xs text-gray-500 mt-1">
              Türkiye'nin en büyük kayak ve doğa sporları merkezi
            </div>
          </div>
          
          <div className="flex justify-center gap-6">
            {Object.entries(groupedRoutes).map(([type, route]) => {
              const IconComponent = route.icon;
              return (
                <div key={type} className="flex flex-col items-center">
                  <div className="flex items-center gap-1 text-sm font-medium text-gray-700 mb-1">
                    <IconComponent className="w-4 h-4" style={{ color: route.color }} />
                    <span>{route.name}</span>
                  </div>
                  <div className="flex items-center gap-1">
                    <div className="w-4 h-1 rounded" style={{ backgroundColor: route.color }}></div>
                    <span className="text-xs text-gray-600">{route.paths.length} rota</span>
                  </div>
                </div>
              );
            })}
          </div>
          
          {/* Ek Bilgiler */}
          <div className="flex justify-center gap-4 mt-3 pt-3 border-t border-gray-200">
            <div className="flex items-center gap-1 text-xs text-gray-500">
              <Thermometer className="w-3 h-3" />
              <span>18°C</span>
            </div>
            <div className="flex items-center gap-1 text-xs text-gray-500">
              <Wind className="w-3 h-3" />
              <span>12 km/h</span>
            </div>
            <div className="flex items-center gap-1 text-xs text-gray-500">
              <Users className="w-3 h-3" />
              <span>247 aktif kullanıcı</span>
            </div>
          </div>
        </div>
      </div>

      {/* Floating Action Buttons */}
      <div className="absolute bottom-6 right-6 z-[1000] space-y-3">
        <button className="bg-white shadow-lg rounded-full p-3 hover:shadow-xl transition-all duration-200 hover:scale-105">
          <Navigation className="w-6 h-6 text-gray-700" />
        </button>
        <button className="bg-white shadow-lg rounded-full p-3 hover:shadow-xl transition-all duration-200 hover:scale-105">
          <Compass className="w-6 h-6 text-gray-700" />
        </button>
      </div>
    </div>
  );
};

// Route Detail Panel Component
const RouteDetailPanel = ({ route, onClose, currentImageIndex, setCurrentImageIndex, favorites, toggleFavorite }) => (
  <div className="absolute right-0 top-0 h-full w-96 bg-white shadow-2xl z-[1000] overflow-hidden">
    <div className="h-full flex flex-col">
      <div className="p-4 border-b border-gray-200 bg-white">
        <div className="flex justify-between items-center">
          <h3 className="font-bold text-gray-800">Rota Detayı</h3>
          <button onClick={onClose} className="p-2 hover:bg-gray-100 rounded-lg">
            <X className="w-5 h-5" />
          </button>
        </div>
      </div>

      <div className="flex-1 overflow-y-auto">
        {/* Fotoğraf Slider */}
        {route.resimler && route.resimler.length > 0 && (
          <div className="relative h-64 bg-gray-200">
            <img 
              src={route.resimler[currentImageIndex]} 
              alt={route.adi}
              className="w-full h-full object-cover"
            />
            
            {route.resimler.length > 1 && (
              <>
                <div className="absolute inset-0 flex items-center justify-between p-4">
                  <button 
                    onClick={() => setCurrentImageIndex((prev) => 
                      prev === 0 ? route.resimler.length - 1 : prev - 1
                    )}
                    className="p-2 bg-black/50 text-white rounded-full hover:bg-black/70"
                  >
                    <ChevronRight className="w-4 h-4 rotate-180" />
                  </button>
                  <button 
                    onClick={() => setCurrentImageIndex((prev) => 
                      prev === route.resimler.length - 1 ? 0 : prev + 1
                    )}
                    className="p-2 bg-black/50 text-white rounded-full hover:bg-black/70"
                  >
                    <ChevronRight className="w-4 h-4" />
                  </button>
                </div>
                
                <div className="absolute bottom-4 left-1/2 transform -translate-x-1/2 flex gap-2">
                  {route.resimler.map((_, index) => (
                    <button
                      key={index}
                      onClick={() => setCurrentImageIndex(index)}
                      className={`w-2 h-2 rounded-full transition-all ${
                        index === currentImageIndex ? 'bg-white' : 'bg-white/50'
                      }`}
                    />
                  ))}
                </div>
              </>
            )}

            <button
              onClick={() => toggleFavorite(route.id)}
              className="absolute top-4 right-4 p-2 bg-black/50 text-white rounded-full hover:bg-black/70"
            >
              <Heart 
                className={`w-5 h-5 ${
                  favorites.includes(route.id) ? 'fill-red-500 text-red-500' : 'text-white'
                }`} 
              />
            </button>
          </div>
        )}

        {/* Rota Bilgileri */}
        <div className="p-6 space-y-6">
          <div>
            <h2 className="text-xl font-bold text-gray-800 mb-2">{route.adi}</h2>
            <p className="text-gray-600 text-sm leading-relaxed">{route.aciklama}</p>
          </div>

          {/* İstatistikler */}
          <div className="grid grid-cols-2 gap-4">
            <div className="bg-blue-50 rounded-lg p-4">
              <div className="flex items-center gap-2 mb-1">
                <MapPin className="w-4 h-4 text-blue-600" />
                <span className="text-xs font-medium text-blue-600 uppercase">Mesafe</span>
              </div>
              <div className="text-lg font-bold text-gray-800">{route.mesafe} km</div>
            </div>

            <div className="bg-green-50 rounded-lg p-4">
              <div className="flex items-center gap-2 mb-1">
                <Clock className="w-4 h-4 text-green-600" />
                <span className="text-xs font-medium text-green-600 uppercase">Süre</span>
              </div>
              <div className="text-lg font-bold text-gray-800">
                {route.tahminiSureDakika >= 60 ? 
                  `${Math.floor(route.tahminiSureDakika / 60)}s ${route.tahminiSureDakika % 60}dk` : 
                  `${route.tahminiSureDakika}dk`}
              </div>
            </div>

            <div className="bg-purple-50 rounded-lg p-4">
              <div className="flex items-center gap-2 mb-1">
                <Mountain className="w-4 h-4 text-purple-600" />
                <span className="text-xs font-medium text-purple-600 uppercase">Yükseklik</span>
              </div>
              <div className="text-lg font-bold text-gray-800">{route.yukseklik}m</div>
            </div>

            <div className="bg-orange-50 rounded-lg p-4">
              <div className="flex items-center gap-2 mb-1">
                <Award className="w-4 h-4 text-orange-600" />
                <span className="text-xs font-medium text-orange-600 uppercase">Zorluk</span>
              </div>
              <div className="text-lg font-bold text-gray-800">{route.zorluk}</div>
            </div>
          </div>

          {/* Özellikler */}
          {route.ozellikler && route.ozellikler.length > 0 && (
            <div>
              <h4 className="font-semibold text-gray-800 mb-3">Özellikler</h4>
              <div className="flex flex-wrap gap-2">
                {route.ozellikler.map((ozellik, index) => (
                  <span 
                    key={index} 
                    className="px-3 py-1 bg-gray-100 text-gray-700 rounded-full text-sm font-medium"
                  >
                    {ozellik}
                  </span>
                ))}
              </div>
            </div>
          )}

          {/* Aksiyon Butonları */}
          <div className="space-y-3">
            <button className="w-full bg-green-500 hover:bg-green-600 text-white py-3 rounded-lg font-medium flex items-center justify-center gap-2 transition-colors">
              <Navigation className="w-5 h-5" />
              Rotayı Başlat
            </button>
            
            <div className="grid grid-cols-3 gap-2">
              <button className="flex items-center justify-center gap-1 px-3 py-2 bg-gray-100 hover:bg-gray-200 rounded-lg transition-colors">
                <Share2 className="w-4 h-4" />
                <span className="text-sm">Paylaş</span>
              </button>
              <button className="flex items-center justify-center gap-1 px-3 py-2 bg-gray-100 hover:bg-gray-200 rounded-lg transition-colors">
                <Download className="w-4 h-4" />
                <span className="text-sm">İndir</span>
              </button>
              <button className="flex items-center justify-center gap-1 px-3 py-2 bg-gray-100 hover:bg-gray-200 rounded-lg transition-colors">
                <Camera className="w-4 h-4" />
                <span className="text-sm">Fotoğraf</span>
              </button>
            </div>
          </div>

          {/* Güvenlik Bilgileri */}
          <div className="bg-yellow-50 border border-yellow-200 rounded-lg p-4">
            <div className="flex items-center gap-2 mb-2">
              <Shield className="w-5 h-5 text-yellow-600" />
              <h4 className="font-semibold text-yellow-800">Güvenlik Önerileri</h4>
            </div>
            <ul className="text-sm text-yellow-700 space-y-1">
              <li>• Yeterli su ve yiyecek alın</li>
              <li>• Hava durumunu kontrol edin</li>
              <li>• Uygun kıyafet giyin</li>
              <li>• Grup halinde gidin</li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </div>
);

// Login Form Component
const LoginForm = ({ onLogin, onSwitchToRegister }) => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);

  const handleSubmit = (e) => {
    e.preventDefault();
    onLogin(email, password);
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">E-posta</label>
        <div className="relative">
          <Mail className="absolute left-3 top-1/2 transform -translate-y-1/2 w-5 h-5 text-gray-400" />
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            className="w-full pl-10 pr-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent outline-none"
            placeholder="ornek@email.com"
            required
          />
        </div>
      </div>

      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">Şifre</label>
        <div className="relative">
          <Lock className="absolute left-3 top-1/2 transform -translate-y-1/2 w-5 h-5 text-gray-400" />
          <input
            type={showPassword ? 'text' : 'password'}
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            className="w-full pl-10 pr-12 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent outline-none"
            placeholder="Şifreniz"
            required
          />
          <button
            type="button"
            onClick={() => setShowPassword(!showPassword)}
            className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-gray-600"
          >
            {showPassword ? <EyeOff className="w-5 h-5" /> : <Eye className="w-5 h-5" />}
          </button>
        </div>
      </div>

      <button
        type="submit"
        className="w-full bg-green-500 hover:bg-green-600 text-white py-3 rounded-lg font-medium transition-colors"
      >
        Giriş Yap
      </button>

      <div className="text-center">
        <span className="text-gray-600">Hesabınız yok mu? </span>
        <button
          type="button"
          onClick={onSwitchToRegister}
          className="text-green-600 hover:text-green-700 font-medium"
        >
          Kayıt olun
        </button>
      </div>
    </form>
  );
};

// Register Form Component
const RegisterForm = ({ onRegister, onSwitchToLogin }) => {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    phone: '',
    password: '',
    confirmPassword: ''
  });
  const [showPassword, setShowPassword] = useState(false);

  const handleSubmit = (e) => {
    e.preventDefault();
    if (formData.password !== formData.confirmPassword) {
      alert('Şifreler eşleşmiyor!');
      return;
    }
    onRegister(formData);
  };

  const handleInputChange = (field, value) => {
    setFormData(prev => ({ ...prev, [field]: value }));
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">Ad Soyad</label>
        <div className="relative">
          <User className="absolute left-3 top-1/2 transform -translate-y-1/2 w-5 h-5 text-gray-400" />
          <input
            type="text"
            value={formData.name}
            onChange={(e) => handleInputChange('name', e.target.value)}
            className="w-full pl-10 pr-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent outline-none"
            placeholder="Adınız Soyadınız"
            required
          />
        </div>
      </div>

      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">E-posta</label>
        <div className="relative">
          <Mail className="absolute left-3 top-1/2 transform -translate-y-1/2 w-5 h-5 text-gray-400" />
          <input
            type="email"
            value={formData.email}
            onChange={(e) => handleInputChange('email', e.target.value)}
            className="w-full pl-10 pr-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent outline-none"
            placeholder="ornek@email.com"
            required
          />
        </div>
      </div>

      <div>
        <label className="block text-sm font-medium text-gray-700 mb-2">Şifre</label>
        <div className="relative">
          <Lock className="absolute left-3 top-1/2 transform -translate-y-1/2 w-5 h-5 text-gray-400" />
          <input
            type={showPassword ? 'text' : 'password'}
            value={formData.password}
            onChange={(e) => handleInputChange('password', e.target.value)}
            className="w-full pl-10 pr-12 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent outline-none"
            placeholder="Güçlü bir şifre"
            required
          />
          <button
            type="button"
            onClick={() => setShowPassword(!showPassword)}
            className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-gray-600"
          >
            {showPassword ? <EyeOff className="w-5 h-5" /> : <Eye className="w-5 h-5" />}
          </button>
        </div>
      </div>

      <button
        type="submit"
        className="w-full bg-green-500 hover:bg-green-600 text-white py-3 rounded-lg font-medium transition-colors"
      >
        Kayıt Ol
      </button>

      <div className="text-center">
        <span className="text-gray-600">Zaten hesabınız var mı? </span>
        <button
          type="button"
          onClick={onSwitchToLogin}
          className="text-green-600 hover:text-green-700 font-medium"
        >
          Giriş yapın
        </button>
      </div>
    </form>
  );
};

// User Menu Component
const UserMenu = ({ user, onLogout }) => {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <div className="relative">
      <button
        onClick={() => setIsOpen(!isOpen)}
        className="bg-white shadow-lg rounded-full p-3 hover:shadow-xl transition-all duration-200 hover:scale-105 flex items-center gap-2"
      >
        <div className="w-8 h-8 bg-green-500 rounded-full flex items-center justify-center text-white font-medium">
          {user.name.charAt(0)}
        </div>
      </button>

      {isOpen && (
        <div className="absolute right-0 mt-2 w-64 bg-white rounded-2xl shadow-2xl border border-gray-100 overflow-hidden">
          <div className="p-4 border-b border-gray-100">
            <div className="flex items-center gap-3">
              <div className="w-12 h-12 bg-green-500 rounded-full flex items-center justify-center text-white font-bold text-lg">
                {user.name.charAt(0)}
              </div>
              <div>
                <div className="font-semibold text-gray-800">{user.name}</div>
                <div className="text-sm text-gray-600">{user.email}</div>
              </div>
            </div>
          </div>

          <div className="p-2">
            <MenuButton icon={User} text="Profil" />
            <MenuButton icon={Bookmark} text="Favoriler" />
            <MenuButton icon={History} text="Rotalarım" />
            <MenuButton icon={Settings} text="Ayarlar" />
            
            <div className="border-t border-gray-100 mt-2 pt-2">
              <button
                onClick={onLogout}
                className="w-full flex items-center gap-3 px-3 py-2 text-red-600 hover:bg-red-50 rounded-lg transition-colors"
              >
                <X className="w-5 h-5" />
                <span>Çıkış Yap</span>
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

// Menu Button Component
const MenuButton = ({ icon: Icon, text }) => (
  <button className="w-full flex items-center gap-3 px-3 py-2 text-gray-700 hover:bg-gray-50 rounded-lg transition-colors">
    <Icon className="w-5 h-5" />
    <span>{text}</span>
  </button>
);

export default UludagParkMap;