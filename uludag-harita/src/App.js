import React, { useState, useEffect } from 'react';
import { Search, Menu, X, MapPin, Clock, Flower2, Navigation, User, Settings, Star, History, Bookmark, Mountain, Car, Bike, Footprints, Zap } from 'lucide-react';

const UludagParkMap = () => {
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [userMenuOpen, setUserMenuOpen] = useState(false);
  const [selectedRoute, setSelectedRoute] = useState('');
  const [activeLayer, setActiveLayer] = useState('all');
  const [searchQuery, setSearchQuery] = useState('');

  // Örnek rota verileri
  const routes = {
    walking: {
      name: 'Yürüyüş Rotaları',
      color: '#22c55e',
      icon: Footprints,
      paths: [
        { id: 1, name: 'Sarıalan Yürüyüş Rotası', distance: '3.2 km', duration: '1.5 saat', difficulty: 'Kolay' },
        { id: 2, name: 'Zirve Tırmanış Rotası', distance: '8.7 km', duration: '4 saat', difficulty: 'Zor' },
        { id: 3, name: 'Orman İçi Doğa Yürüyüşü', distance: '5.4 km', duration: '2.5 saat', difficulty: 'Orta' }
      ]
    },
    cycling: {
      name: 'Bisiklet Rotaları',
      color: '#3b82f6',
      icon: Bike,
      paths: [
        { id: 4, name: 'Dağ Bisikleti Rotası', distance: '12.3 km', duration: '2 saat', difficulty: 'Zor' },
        { id: 5, name: 'Aile Bisiklet Turu', distance: '6.8 km', duration: '1.5 saat', difficulty: 'Kolay' }
      ]
    },
    driving: {
      name: 'Araç Rotaları',
      color: '#ef4444',
      icon: Car,
      paths: [
        { id: 6, name: 'Panoramik Araç Turu', distance: '18.5 km', duration: '45 dakika', difficulty: 'Kolay' },
        { id: 7, name: 'Kamp Alanları Turu', distance: '25.2 km', duration: '1.5 saat', difficulty: 'Orta' }
      ]
    },
    atv: {
      name: 'ATV Rotaları',
      color: '#f59e0b',
      icon: Zap,
      paths: [
        { id: 8, name: 'Macera ATV Rotası', distance: '15.7 km', duration: '2.5 saat', difficulty: 'Zor' },
        { id: 9, name: 'Keşif ATV Turu', distance: '9.3 km', duration: '1.5 saat', difficulty: 'Orta' }
      ]
    }
  };

  // Güneş tutulması ve çiçek açma bilgileri
  const specialEvents = [
    { id: 1, type: 'eclipse', date: '2025-03-29', time: '10:30', location: 'Sarıalan Bölgesi', description: 'Kısmi Güneş Tutulması' },
    { id: 2, type: 'bloom', date: '2025-04-15', location: 'Kazıklıbent Yaylası', description: 'Uludağ Lale Çiçekleri' },
    { id: 3, type: 'bloom', date: '2025-05-20', location: 'Kirazlıyayla', description: 'Rododendron Çiçekleri' },
    { id: 4, type: 'eclipse', date: '2025-09-17', time: '14:45', location: 'Zirve Bölgesi', description: 'Kısmi Güneş Tutulması' }
  ];

  const recentSearches = ['Sarıalan', 'Oteller Bölgesi', 'Teleferik İstasyonu', 'Kamp Alanları'];
  const savedLocations = ['Ev', 'İş Yeri', 'Favorim 1', 'Favorim 2'];

  return (
    <div className="h-screen w-full bg-gray-50 relative overflow-hidden">
      {/* Ana Harita Alanı */}
      <div className="absolute inset-0 bg-gradient-to-br from-emerald-100 via-blue-50 to-green-100">
        {/* Harita Simülasyonu */}
        <div className="w-full h-full relative">
          {/* Uludağ Harita Katmanları */}
          <div className="absolute inset-4 rounded-xl bg-white shadow-lg overflow-hidden">
            {/* Harita Başlığı */}
            <div className="absolute top-4 left-4 z-10 bg-white/90 backdrop-blur-sm rounded-lg p-3 shadow-md">
              <div className="flex items-center gap-2">
                <Mountain className="w-5 h-5 text-green-600" />
                <h3 className="font-bold text-gray-800">Uludağ Milli Parkı</h3>
              </div>
            </div>

            {/* Rota Gösterimi */}
            <svg className="w-full h-full" viewBox="0 0 800 600">
              {/* Arkaplan Desen */}
              <defs>
                <pattern id="topo" x="0" y="0" width="20" height="20" patternUnits="userSpaceOnUse">
                  <circle cx="10" cy="10" r="0.5" fill="#10b98180" />
                </pattern>
              </defs>
              <rect width="800" height="600" fill="url(#topo)" />
              
              {/* Yürüyüş Rotaları */}
              {(activeLayer === 'all' || activeLayer === 'walking') && (
                <>
                  <path d="M100 400 Q200 350 300 380 Q400 420 500 400" 
                        stroke="#22c55e" strokeWidth="3" fill="none" strokeDasharray="5,5" />
                  <path d="M150 200 Q250 150 350 180 Q450 220 550 200 Q650 180 700 220" 
                        stroke="#22c55e" strokeWidth="3" fill="none" strokeDasharray="5,5" />
                </>
              )}
              
              {/* Bisiklet Rotaları */}
              {(activeLayer === 'all' || activeLayer === 'cycling') && (
                <>
                  <path d="M80 300 Q180 250 280 280 Q380 320 480 300 Q580 280 680 320" 
                        stroke="#3b82f6" strokeWidth="4" fill="none" />
                </>
              )}
              
              {/* Araç Rotaları */}
              {(activeLayer === 'all' || activeLayer === 'driving') && (
                <>
                  <path d="M50 450 Q150 400 250 430 Q350 470 450 450 Q550 430 650 470 Q750 450 780 480" 
                        stroke="#ef4444" strokeWidth="5" fill="none" />
                </>
              )}
              
              {/* ATV Rotaları */}
              {(activeLayer === 'all' || activeLayer === 'atv') && (
                <>
                  <path d="M120 500 Q220 450 320 480 Q420 520 520 500" 
                        stroke="#f59e0b" strokeWidth="4" fill="none" strokeDasharray="10,5" />
                </>
              )}
              
              {/* Özel Etkinlik Noktaları */}
              {specialEvents.map((event, index) => (
                <g key={event.id}>
                  <circle cx={200 + index * 150} cy={150 + index * 80} r="8" 
                          fill={event.type === 'eclipse' ? '#fbbf24' : '#ec4899'} 
                          stroke="white" strokeWidth="2" />
                  {event.type === 'eclipse' ? (
                    <Clock className="w-4 h-4" x={196 + index * 150} y={146 + index * 80} />
                  ) : (
                    <Flower2 className="w-4 h-4" x={196 + index * 150} y={146 + index * 80} />
                  )}
                </g>
              ))}
            </svg>
          </div>
        </div>
      </div>

      {/* Sol Sidebar */}
      <div className={`absolute left-0 top-0 h-full bg-white shadow-2xl z-20 transition-all duration-300 ease-in-out ${
        sidebarOpen ? 'w-96' : 'w-0'
      } overflow-hidden`}>
        <div className="p-6 h-full overflow-y-auto">
          {/* Başlık */}
          <div className="flex items-center justify-between mb-6">
            <h2 className="text-2xl font-bold text-gray-800">Uludağ Keşfi</h2>
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
              placeholder="Konum ara..."
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
              className="w-full pl-10 pr-4 py-3 border border-gray-200 rounded-xl focus:ring-2 focus:ring-green-500 focus:border-transparent outline-none"
            />
          </div>

          {/* Aktif Rota Bilgileri */}
          {activeLayer !== 'all' && routes[activeLayer] && (() => {
            const ActiveRouteIcon = routes[activeLayer].icon;
            return (
              <div className="bg-gradient-to-r from-green-50 to-blue-50 rounded-xl p-4 mb-6 border-2 border-green-200">
                <div className="flex items-center gap-3 mb-3">
                  <ActiveRouteIcon className="w-6 h-6" style={{ color: routes[activeLayer].color }} />
                  <h3 className="font-bold text-gray-800">{routes[activeLayer].name}</h3>
                </div>
                <p className="text-sm text-gray-600">Seçili rota tipi haritada vurgulandı</p>
              </div>
            );
          })()}

          {/* Detaylı Rota Listesi */}
          {activeLayer !== 'all' && routes[activeLayer] && (
            <div className="space-y-3 mb-8">
              <h4 className="font-semibold text-gray-700 flex items-center gap-2">
                <MapPin className="w-4 h-4" />
                Mevcut Rotalar
              </h4>
              {routes[activeLayer].paths.map((path) => (
                <div key={path.id} className="bg-white rounded-lg p-4 shadow-sm hover:shadow-md transition-all cursor-pointer border border-gray-100 hover:border-gray-200">
                  <div className="flex justify-between items-start mb-2">
                    <h5 className="font-medium text-gray-800">{path.name}</h5>
                    <span className={`px-2 py-1 rounded-full text-xs font-medium ${
                      path.difficulty === 'Kolay' ? 'bg-green-100 text-green-700' :
                      path.difficulty === 'Orta' ? 'bg-yellow-100 text-yellow-700' :
                      'bg-red-100 text-red-700'
                    }`}>
                      {path.difficulty}
                    </span>
                  </div>
                  <div className="text-sm text-gray-600 space-y-1">
                    <div className="flex justify-between">
                      <span>Mesafe:</span>
                      <span className="font-medium">{path.distance}</span>
                    </div>
                    <div className="flex justify-between">
                      <span>Süre:</span>
                      <span className="font-medium">{path.duration}</span>
                    </div>
                  </div>
                </div>
              ))}
            </div>
          )}

          {/* Özel Etkinlikler */}
          <div className="space-y-3 mb-8">
            <h3 className="font-semibold text-gray-700 text-sm uppercase tracking-wide">Özel Etkinlikler</h3>
            {specialEvents.map((event) => (
              <div key={event.id} className="bg-gradient-to-r from-purple-50 to-pink-50 rounded-lg p-4 border border-purple-100">
                <div className="flex items-start gap-3">
                  {event.type === 'eclipse' ? (
                    <Clock className="w-5 h-5 text-yellow-600 mt-0.5" />
                  ) : (
                    <Flower2 className="w-5 h-5 text-pink-600 mt-0.5" />
                  )}
                  <div className="flex-1">
                    <h5 className="font-medium text-gray-800 mb-1">{event.description}</h5>
                    <p className="text-sm text-gray-600 mb-1">{event.location}</p>
                    <div className="text-xs text-gray-500">
                      {event.date} {event.time && `- ${event.time}`}
                    </div>
                  </div>
                </div>
              </div>
            ))}
          </div>

          {/* Son Aramalar */}
          <div className="space-y-3 mb-6">
            <h3 className="font-semibold text-gray-700 text-sm uppercase tracking-wide flex items-center gap-2">
              <History className="w-4 h-4" />
              Son Aramalar
            </h3>
            {recentSearches.map((search, index) => (
              <button key={index} className="w-full text-left p-3 hover:bg-gray-50 rounded-lg transition-colors text-gray-600">
                {search}
              </button>
            ))}
          </div>

          {/* Kaydedilen Konumlar */}
          <div className="space-y-3">
            <h3 className="font-semibold text-gray-700 text-sm uppercase tracking-wide flex items-center gap-2">
              <Bookmark className="w-4 h-4" />
              Kaydedilen Konumlar
            </h3>
            {savedLocations.map((location, index) => (
              <button key={index} className="w-full text-left p-3 hover:bg-gray-50 rounded-lg transition-colors text-gray-600 flex items-center gap-2">
                <Star className="w-4 h-4 text-yellow-500" />
                {location}
              </button>
            ))}
          </div>
        </div>
      </div>

      {/* Menu Toggle Button */}
      <button
        onClick={() => setSidebarOpen(!sidebarOpen)}
        className="absolute top-6 left-6 z-30 bg-white shadow-lg rounded-xl p-3 hover:shadow-xl transition-all duration-200 hover:scale-105"
      >
        <Menu className="w-6 h-6 text-gray-700" />
      </button>

      {/* Rota Kontrolleri - Sol Üst */}
      <div className="absolute top-6 left-20 z-20 flex gap-2">
        {Object.entries(routes).map(([key, route]) => {
          const IconComponent = route.icon;
          return (
            <button
              key={key}
              onClick={() => setActiveLayer(activeLayer === key ? 'all' : key)}
              className={`flex items-center gap-2 px-4 py-2 rounded-xl transition-all duration-200 font-medium text-sm shadow-lg hover:shadow-xl hover:scale-105 ${
                activeLayer === key 
                  ? 'bg-white text-gray-800 ring-2 ring-opacity-50' 
                  : 'bg-white/90 backdrop-blur-sm text-gray-700 hover:bg-white'
              }`}
              style={activeLayer === key ? { ringColor: route.color } : {}}
            >
              <IconComponent className="w-4 h-4" style={{ color: route.color }} />
              <span className="hidden sm:block">{route.name.replace(' Rotaları', '')}</span>
            </button>
          );
        })}
        
        {/* Tümünü Göster Butonu */}
        <button
          onClick={() => setActiveLayer('all')}
          className={`px-4 py-2 rounded-xl transition-all duration-200 font-medium text-sm shadow-lg hover:shadow-xl hover:scale-105 ${
            activeLayer === 'all' 
              ? 'bg-gradient-to-r from-green-500 to-blue-600 text-white' 
              : 'bg-white/90 backdrop-blur-sm text-gray-700 hover:bg-white'
          }`}
        >
          Tümü
        </button>
      </div>

      {/* Kullanıcı Menüsü */}
      <div className="absolute top-6 right-6 z-30">
        <button
          onClick={() => setUserMenuOpen(!userMenuOpen)}
          className="bg-white shadow-lg rounded-full p-3 hover:shadow-xl transition-all duration-200 hover:scale-105"
        >
          <User className="w-6 h-6 text-gray-700" />
        </button>

        {/* Kullanıcı Dropdown */}
        {userMenuOpen && (
          <div className="absolute right-0 mt-2 w-64 bg-white rounded-2xl shadow-2xl border border-gray-100 overflow-hidden">
            <div className="p-6 bg-gradient-to-r from-green-500 to-blue-600 text-white">
              <div className="flex items-center gap-3">
                <div className="w-12 h-12 bg-white/20 rounded-full flex items-center justify-center">
                  <User className="w-6 h-6" />
                </div>
                <div>
                  <h3 className="font-semibold">Kullanıcı Adı</h3>
                  <p className="text-sm opacity-90">kullanici@email.com</p>
                </div>
              </div>
            </div>
            
            <div className="p-2">
              <button className="w-full flex items-center gap-3 p-3 hover:bg-gray-50 rounded-lg transition-colors text-left">
                <User className="w-4 h-4 text-gray-600" />
                <span className="text-gray-700">Profil</span>
              </button>
              <button className="w-full flex items-center gap-3 p-3 hover:bg-gray-50 rounded-lg transition-colors text-left">
                <Settings className="w-4 h-4 text-gray-600" />
                <span className="text-gray-700">Ayarlar</span>
              </button>
              <button className="w-full flex items-center gap-3 p-3 hover:bg-gray-50 rounded-lg transition-colors text-left">
                <Navigation className="w-4 h-4 text-gray-600" />
                <span className="text-gray-700">Rotalarım</span>
              </button>
              <hr className="my-2" />
              <button className="w-full flex items-center gap-3 p-3 hover:bg-red-50 rounded-lg transition-colors text-left text-red-600">
                <span>Çıkış Yap</span>
              </button>
            </div>
          </div>
        )}
      </div>

      {/* Konum Butonu */}
      <button className="absolute bottom-20 right-6 bg-white shadow-lg rounded-full p-4 hover:shadow-xl transition-all duration-200 hover:scale-105">
        <Navigation className="w-6 h-6 text-gray-700" />
      </button>

      {/* Alt Bilgi Paneli */}
      <div className="absolute bottom-6 left-1/2 transform -translate-x-1/2 bg-white/90 backdrop-blur-sm rounded-2xl shadow-lg p-4 max-w-md">
        <div className="text-center">
          <h4 className="font-semibold text-gray-800 mb-1">Uludağ Milli Parkı</h4>
          <p className="text-sm text-gray-600">Doğa harikası rotaları keşfedin</p>
          <div className="flex justify-center gap-4 mt-3">
            <div className="flex items-center gap-1 text-xs text-gray-500">
              <div className="w-3 h-0.5 bg-green-500"></div>
              <span>Yürüyüş</span>
            </div>
            <div className="flex items-center gap-1 text-xs text-gray-500">
              <div className="w-3 h-0.5 bg-blue-500"></div>
              <span>Bisiklet</span>
            </div>
            <div className="flex items-center gap-1 text-xs text-gray-500">
              <div className="w-3 h-0.5 bg-red-500"></div>
              <span>Araç</span>
            </div>
            <div className="flex items-center gap-1 text-xs text-gray-500">
              <div className="w-3 h-0.5 bg-yellow-500"></div>
              <span>ATV</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default UludagParkMap;